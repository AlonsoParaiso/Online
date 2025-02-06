using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType {BULLET, BOMB}
public class PoolObject : MonoBehaviour
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
        GameObject clone = Instantiate(objectToPool); // el gameobject
        clone.SetActive(false); // lo activas en falso para que vaya 
        poolList.Add(clone); // lo añadimos a la lista 

        return clone;
    }

    public GameObject GimmeInactiveGameObject()
    {
        foreach (GameObject obj in poolList) // para recorrer la lista 
        {
            if (!obj.activeSelf) // si el objeto no esta activo
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
}
