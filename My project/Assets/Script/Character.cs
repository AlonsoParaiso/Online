using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ClashofWorlds //crea un espacio de nombres para evitar la colision de nombres
{
    public abstract class Character
    {
        //informacion de los players
        protected Animator _animator;
        //private AnimatorController _controller;


        public float speed, damage, health;
        protected GameObject _obj;
        protected Rigidbody _rb;

        public Character(float speed, Rigidbody rb, GameObject obj, float damage, float health) //constructor general de los personajes
        {
            this.speed = speed;
            _rb = rb;
            _obj = obj;
            this.health = health;
            //_controller = cont;

        }
        public GameObject GetObject() { return _obj; } //se genere el sprite


        //public AnimatorController GetAnimatorController() { return _controller; } //animaciones
        public float GetDamage()  // el metodo que aparecera en los hijos para el daño 
        {
            return damage;
        }

        public virtual float Heal() //el metodo que aparecera en los hijos para la vida 

        {
            Debug.Log("Character se cura");
            health = Mathf.Clamp(health, 0, 100);  // lo clampeamos para que al curarse no sobrepase los 100 de vida 
            return health;
        }
        public abstract void Attack(Transform ownerTransform);

        public virtual void ReceiveDamage(float damage)
        {

        }

        public virtual void Death()
        {

        }
        
            
        
    }
}
