using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace MetroidvaniaTools
{
    public class Jump : Abilities
    {
        [SerializeField] protected float jumpForce;
        [SerializeField] protected float distanceToCollider;
        public LayerMask collisionLayer;
        [SerializeField] protected int maxJumps;
        [SerializeField] protected bool limitAirJumps;
        [SerializeField] protected float horizontalWallJumpForce;
        [SerializeField] protected float verticalWallJumpForce;
        [SerializeField] protected float maxJumpSpeed;
        [SerializeField] protected float maxFallSpeed;
        [SerializeField] protected float acceptedFallSpeed;
        [SerializeField] protected float glideTime;
        [SerializeField]
        [Range(-2, 2)] protected float gravity;
        [SerializeField] protected float wallJumpTime;
        [SerializeField] protected float holdForce;
        [SerializeField] protected float buttonHoldTime;

        private bool isWallJumping;
        private int numberOfJumpsLeft;
        private float jumpCountDown;
        private float fallCountDown;

        protected override void Initialization()
        {
            base.Initialization();
            numberOfJumpsLeft = maxJumps;
            jumpCountDown = buttonHoldTime;
            fallCountDown = glideTime;
        }
        // Update is called once per frame
        protected virtual void Update()
        {
            CheckForJump();
            input.JumpHeld();
        }

        protected virtual bool CheckForJump()
        {
            if (input.JumpPressed())
            {
                if (!character.isGrounded && numberOfJumpsLeft == maxJumps)
                {
                    character.isJumping = false;
                    return false;
                }
                if (limitAirJumps && character.Falling(acceptedFallSpeed)) {
                    character.isJumping = false;
                    return false;
                }
                if (character.isWallSliding)
                {
                    isWallJumping = true;
                    return false;
                }
                numberOfJumpsLeft--;
                if (numberOfJumpsLeft >= 0)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    jumpCountDown = buttonHoldTime;
                    character.isJumping = true;
                    fallCountDown = glideTime;
                }
                return true;
            }
            else { return false; }
        }

        protected virtual void FixedUpdate()
        {
            IsJumping();
            Gliding();
            GroundCheck();
            WallSliding();
            WallJump();
        }

        protected virtual void IsJumping()
        {
            if (character.isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce);
                AdditionalAir();
            }
            if (rb.velocity.y > maxJumpSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, maxJumpSpeed);
            }
        }
        protected virtual void Gliding()
        {
            if (character.Falling(0) && input.JumpHeld())
            {
                fallCountDown -= Time.deltaTime;
                if (fallCountDown > 0 && rb.velocity.y > acceptedFallSpeed)
                {
                    FallSpeed(gravity);
                }
            }
        }
        protected virtual void AdditionalAir()
        {
            if (input.JumpHeld())
            {
                jumpCountDown -= Time.deltaTime;
                if (jumpCountDown <= 0)
                {
                    jumpCountDown = 0;
                    character.isJumping = false;
                }
                else
                {
                    rb.AddForce(Vector2.up * holdForce);
                }
            }
            else
            {
                character.isJumping = false;
            }
        }
        protected virtual void GroundCheck()
        {
            if (CollisionCheck(Vector2.down, distanceToCollider, collisionLayer) && !character.isJumping)
            {
                character.isGrounded = true;
                numberOfJumpsLeft = maxJumps;
                fallCountDown = glideTime;
            }
            else
            {
                character.isGrounded = false;
                if (character.Falling(0) && rb.velocity.y < maxFallSpeed) {
                    rb.velocity = new Vector2(rb.velocity.x, maxFallSpeed);
                }
            }
        }
        protected virtual bool WallCheck()
        {
            if ((!character.isFacingLeft && CollisionCheck(Vector2.right, distanceToCollider, collisionLayer) || character.isFacingLeft && CollisionCheck(Vector2.left, distanceToCollider, collisionLayer)) && movement.MovementPressed() && !character.isGrounded)
            {
                return true;
            }
            return false;
        }
        protected virtual bool WallSliding()
        {
            if (WallCheck())
            {
                FallSpeed(gravity);
                character.isWallSliding = true;
                return true;
            }
            else
            {
                character.isWallSliding = false;
                return false;
            }
        }
        protected virtual void WallJump()
        {
            if(isWallJumping)
            {
                rb.AddForce(Vector2.up * verticalWallJumpForce);
                if(!character.isFacingLeft)
                {
                    rb.AddForce(Vector2.left * horizontalWallJumpForce);
                }
                if (character.isFacingLeft)
                {
                    rb.AddForce(Vector2.right * horizontalWallJumpForce);
                }
                StartCoroutine(WallJumped());
            }
        }
        protected virtual IEnumerator WallJumped()
        {
            movement.enabled = false;
            yield return new WaitForSeconds(wallJumpTime);
            movement.enabled = true;
            isWallJumping = false;
        }
    }
}