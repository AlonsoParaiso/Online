using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public abstract class PlayableCharacter : Character
    {          
        public float rate;
        public GameObject bullet; 

        public PlayableCharacter(float speed, float damage, float health, float rate, string prefabPath) : base(speed, damage, health, prefabPath ) //constructor general de los personajes
        {
            bullet = Resources.Load<GameObject>("bullet"); // para crear la bala
            this.rate = rate; 
            //_controller = cont;

        }

        public override void Attack(GameObject owner)
        { 
            GameObject.Instantiate(bullet, owner.transform.position, Quaternion.identity); 
        }
        
    }

