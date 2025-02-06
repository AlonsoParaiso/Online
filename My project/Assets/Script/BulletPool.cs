using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public PoolObject bulletPool;
    public PoolType poolTypeToSearch;

   


    private void Start()
    {
        PoolObject[] pools = FindObjectsOfType<PoolObject>();

        foreach (PoolObject pool in pools)
        {
            if (pool.poolType == poolTypeToSearch)
            {
                bulletPool = pool;
                break;
            }
        }


    }
}
