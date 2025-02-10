using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public enum PoolType {BULLET, BOMB}
public class PoolObject : MonoBehaviourPunCallbacks
{

    private List<GameObject> poolList;
    [Tooltip("Initial pool size")]
    public uint poolSize;
    [Tooltip("If true, size increments")]
    public bool shouldExpand = false; // Por si tenemos que expandir la pool
    [Tooltip("Object to add")]
    public GameObject objectToPool;

    public PoolType poolType; 
    // Start is called before the first frame update
    void Start()
    {
        poolList = new List<GameObject>();
        for (int i = 0; i < poolSize; i++) // para que instancie x objetos al inicio 
        {
            AddGameObject();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    GameObject AddGameObject()
    {
        GameObject clone = PhotonNetwork.Instantiate(this.objectToPool.name,new Vector3(0,0,0),Quaternion.identity,0); // el gameobject
        if (clone != null)
        {
            poolList.Add(clone);
        }

        return clone;
    }

    public GameObject GimmeInactiveGameObject()
    {
        foreach (GameObject obj in poolList) // para recorrer la lista 
        {
            if (obj.GetComponent<PunPoolObject>().readyToUse) // si el objeto no esta activo
            {
                return obj;
            }
        }
        if (shouldExpand) // si queremos expandir la pool 
        {
            return AddGameObject();
        }
        return null;
    }

    public void DelayInstantiateObjects()
    {
        StartCoroutine(InstantiateObjects());
    }

    IEnumerator InstantiateObjects()
    {
        yield return new WaitForSeconds(0.3F);

        poolList.Clear();
        for (int i = 0; i < poolSize; i++) // para que instancie x objetos al inicio 
        {
            AddGameObject();
        }
    }
}
