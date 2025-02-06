using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;




    public class GhostFace : PlayableCharacter
    {
        public GhostFace(float speed, string prefabPath) : base(5,5,100,5, prefabPath)
        {
         
        }
    public override void Attack(Transform owner)
    {
        BulletSpawn(owner.transform, owner.GetComponent<BulletPool>().bulletPool);
    }

    void BulletSpawn(Transform transform, PoolObject bulletPool)
    {
        GameObject obj = bulletPool.GimmeInactiveGameObject(); 
        if (obj)
        {
            obj.SetActive(true);

            obj.transform.position = new Vector3(transform.transform.position.x + 2, transform.transform.position.y + 2, transform.transform.position.z + 2);

        }
    }
}

