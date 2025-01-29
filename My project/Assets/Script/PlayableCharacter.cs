using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClashofWorlds //crea un espacio de nombres para evitar la colision de nombres
{
    public abstract class PlayableCharacter : Character
    {          
        public float rate;
        public GameObject bullet; 

        public PlayableCharacter(float speed, Rigidbody rb, GameObject obj , float damage, float health, float rate) : base(speed, rb, obj, damage, health)//constructor general de los personajes
        {
            bullet = Resources.Load<GameObject>("bullet"); // para crear la bala
            this.rate = rate; 
            //_controller = cont;

        }

        public override void Attack(Transform ownerTransform)
        { 
            GameObject.Instantiate(bullet, ownerTransform.position, Quaternion.identity); 
        }
        
    }
}
