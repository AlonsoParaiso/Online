using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ClashofWorlds
{
    public class GhostFace : PlayableCharacter
    {
        public GhostFace(float speed, Rigidbody rb) : base(speed, rb, Resources.Load<GameObject>("GhostFace"), 15, 100, 2)
        {
        }

       
    }
}
