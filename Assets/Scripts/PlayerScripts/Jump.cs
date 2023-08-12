using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MetroidvaniaTools
{
    public class Jump : Abilities
    {
        [SerializeField] protected float jumpForce;
        [SerializeField] protected float distanceToCollider;
        [SerializeField] protected LayerMask collisionLayer;
        private bool isJumping;

        // Update is called once per frame
        protected virtual void Update()
        {
            jumpPressed();
        }

        protected virtual bool jumpPressed()
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                return true;
            }
            else { return false; }
        }

        protected virtual void FixedUpdate()
        {
            IsJumping();
        }

        protected virtual void IsJumping()
        {

        }
    }
}