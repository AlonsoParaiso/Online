using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : PlayableCharacter
{
    public Bomba(float speed, string prefabPath) : base(5,10,100,5, prefabPath)
    {
    }
    public override void Attack(GameObject owner)
    {
        BulletSpawn(owner.transform, owner.GetComponentInChildren<BombPool>().bombPool);
    }

    void BulletSpawn(Transform transform, PoolObject BombPool)
    {
        GameObject obj = BombPool.GimmeInactiveGameObject();
        if (obj)
        {
            obj.SetActive(true);

            obj.transform.position = new Vector3(transform.transform.position.x, transform.transform.position.y + 1, transform.transform.position.z);
            obj.transform.rotation = transform.transform.rotation;
            
            Bomb bomb = obj.GetComponent<Bomb>();
            bomb.ResetVelocity();
            bomb.ApplyParabolicThrow(transform);
            obj.GetComponent<PunPoolObject>().readyToUse = false;

            
        }
    }
}
