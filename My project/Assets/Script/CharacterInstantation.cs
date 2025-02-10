using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.PunBasics;
using Photon.Pun;
using UnityEngine;



public class CharacterInstantation : MonoBehaviour
{
    Character character;

    // Start is called before the first frame update
    void Start()
    {
        
        switch (GameManager.instance.characterIndex)
        {
            case 0:
                character = new GhostFace(5,"ghostface");
                break;

            case 1:
                character = new Bomba(10, "bomba");
                break;
        }
        //EL PREFAB NO SE PUEDE CARGAR DE PRIMERAS DEBIDO A QUE SI LO CARGAS 2 VECES DA ERROS PQ NO HAY QUE LEERLO 2 VECES
        if (PlayerManager.localPlayerInstance == null)
        {
            StartCoroutine(WaitInstantiate());
                }
        else

        {
            Debug.LogFormat("Ignoring scene load for {0}", SceneManagerHelper.ActiveSceneName);
        }
        //Instantiate(character.GetGO(), new Vector3(0, 0, 0), Quaternion.identity);
        IEnumerator WaitInstantiate()
        {
            yield return new WaitForSeconds(0.3f);
            GameManager.instance.SetCharacter(character);
            Debug.LogFormat("We are Instantiating LocalPlayer from {0}", SceneManagerHelper.ActiveSceneName);

            PhotonNetwork.Instantiate(character.GetprefabPath(), new Vector3(0f, 5f, 0f), Quaternion.identity, 0);//Eto va debido a que lo cargamos con la escena ya cargada y emtpmnces ya va bien
        }
    }
}
