using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{

    public GameObject controlPanel, loadingPanel;

    // Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon,
    // we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
    // Typically this is used for the OnConnectedToMaster() callback.

    bool isConnecting;
    #region Private Serializable Fields

    [SerializeField]
    private byte maxPlayersPerRoom = 4;

    #endregion

    #region Private Fields


    // This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).

    string gameVersion = "alpha";

    #endregion

    #region MonoBehaviour CallBacks

    private void Awake()
    {
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {    
        loadingPanel.SetActive(false);
        controlPanel.SetActive(true);
    }
    #endregion

    #region Public Methods

    // Start the connection process.
    // - If already connected, we attempt joining a random room
    // - if not yet connected, Connect this application instance to Photon Cloud Network

    public void Connect()
    {

        loadingPanel.SetActive(true);
        controlPanel.SetActive(false);
        // we check if we are connected or not, we join if we are , else we initiate the connection to the server.
        if (PhotonNetwork.IsConnected)  
        {
            PhotonNetwork.JoinRandomRoom();

        }
        else
        {
            // #Critical, we must first and foremost connect to Photon Online Server.
            PhotonNetwork.GameVersion = gameVersion;
            isConnecting = PhotonNetwork.ConnectUsingSettings();
        }
    
        
    }
    #endregion

    #region MonoBehaviourPunCallbacks CallBacks

    public override void OnConnectedToMaster()
    {
        // we don't want to do anything if we are not attempting to join a room.
        // this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
        // we don't want to do anything.
        if (isConnecting)
        {
            // #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnJoinRandomFailed()
            PhotonNetwork.JoinRandomRoom();
            isConnecting = false;
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        isConnecting = false;
        Debug.LogWarningFormat("on disconenctes fue llamado por PUN por la razon{0} ", cause);

    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("PUN Basics Tutorial/Launcher:OnJoinRandomFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom");

        // #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
    }

    public override void OnJoinedRoom()
    {
        // #Critical: We only load if we are the first player, else we rely on `PhotonNetwork.AutomaticallySyncScene` to sync our instance scene.
        if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
        {
            Debug.Log("We load the 'Room for 1' ");

            // #Critical
            // Load the Room Level.
            PhotonNetwork.LoadLevel("Room for 1");
        }
        Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");
    }



    #endregion


}
