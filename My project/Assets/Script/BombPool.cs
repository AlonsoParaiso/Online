using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombPool : MonoBehaviour
{
    public PoolObject bombPool;
    public PoolType poolTypeToSearch;

    private void Start()
    {
        PoolObject[] pools = FindObjectsOfType<PoolObject>();

        foreach (PoolObject pool in pools)
        {
            if (pool.poolType == poolTypeToSearch)
            {
                bombPool = pool;
                break;
            }
        }


    }
}
