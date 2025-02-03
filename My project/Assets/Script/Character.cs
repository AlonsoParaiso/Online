using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public abstract class Character
    {
        //informacion de los players
        protected Animator _animator;
        //private AnimatorController _controller;


        public float speed, damage, health;
        private string prefabPath;

    public Character(float speed, float damage, float health, string prefabPath) //constructor general de los personajes
        {
            this.speed = speed;
            this.health = health;
            this.prefabPath = prefabPath;
        //_controller = cont;

    }
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
        public string GetprefabPath() { return prefabPath; }

}

