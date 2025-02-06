using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PunPoolObject : MonoBehaviourPunCallbacks, IPunObservable
{
    // lo asignamos al prefab de la bala 
    public bool readyToUse = true;
public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(readyToUse);
            gameObject.SetActive(!readyToUse);//corrutina
        }
        else
        {
            readyToUse = (bool)stream.ReceiveNext();
            gameObject.SetActive(!readyToUse);
        }
    }
    //bala 
   
}
