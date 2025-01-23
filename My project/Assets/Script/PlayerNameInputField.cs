using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using TMPro;



// Player name input field. Let the user input his name, will appear above the player in the game.

[RequireComponent(typeof(TMP_InputField))]
public class PlayerNameInputField : MonoBehaviour
{
    #region Private Constants

    // Store the PlayerPref Key to avoid typos
    const string playerNamePrefKey = "PlayerName";

    #endregion

    #region MonoBehaviour CallBacks

    // MonoBehaviour method called on GameObject by Unity during initialization phase.

    void Start()
    {

        string defaultName = "ShroomCriminal";
        TMP_InputField _inputField = GetComponent<TMP_InputField>();
        if (_inputField)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                _inputField.text = defaultName;
            }
        }

        PhotonNetwork.NickName = defaultName;
    }

    #endregion

    #region Public Methods


    // Sets the name of the player, and save it in the PlayerPrefs for future sessions.

    // <param name="value">The name of the Player</param>
    public void SetPlayerName()
    {
        TMP_InputField _inputField = GetComponent<TMP_InputField>();
        string playerName = _inputField.text;
        // #Important
        if (!string.IsNullOrEmpty(playerName))
        {
            PlayerPrefs.SetString(playerNamePrefKey, playerName);
            PhotonNetwork.NickName = playerName;
        }

    }

    #endregion
}
