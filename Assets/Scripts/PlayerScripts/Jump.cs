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
        [SerializeField] protected int maxJumps;
        [SerializeField] protected bool limitAirJumps;
        [SerializeField] protected float maxJumpSpeed;
        [SerializeField] protected float maxFallSpeed;
        [SerializeField] protected float acceptedFallSpeed;

        private bool isJumping;
        private int numberOfJumpsLeft;

        protected override void Initialization()
        {
            base.Initialization();
            numberOfJumpsLeft = maxJumps;
        }
        // Update is called once per frame
        protected virtual void Update()
        {
            JumpPressed();
        }

        protected virtual bool JumpPressed()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (!character.isGrounded && numberOfJumpsLeft == maxJumps)
                {
                    isJumping = false;
                    return false;
                }
                if (limitAirJumps && Falling(acceptedFallSpeed)){
                    isJumping = false;
                    return false; 
                }
                numberOfJumpsLeft--;
                if (numberOfJumpsLeft >= 0)
                {
                    isJumping = true;
                }
                return true;
            }
            else { return false; }
        }

        protected virtual void FixedUpdate()
        {
            IsJumping();
            GroundCheck();
        }

        protected virtual void IsJumping()
        {
            if (isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce);
            }
            if(rb.velocity.y > maxJumpSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxJumpSpeed);
            }
        }
        protected virtual void GroundCheck()
        {
            if (CollisionCheck(Vector2.down, distanceToCollider, collisionLayer) && !isJumping)
            {
                character.isGrounded = true;
                numberOfJumpsLeft = maxJumps;
            }
            else
            {
                character.isGrounded = false;
                isJumping = false;
                if (Falling(0) && rb.velocity.y < maxFallSpeed) {
                    rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
                }
            }
        }
    }
}