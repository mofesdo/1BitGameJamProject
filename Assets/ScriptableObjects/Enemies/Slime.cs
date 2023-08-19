using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace MetroidvaniaTools
{
    public class Slime : MonoBehaviour
    {
        public float jumpForce = 5f;
        public float attackCooldown = 2f;
        public LayerMask playerLayer;  // The layer(s) representing the player

        private Rigidbody2D rb;
        private bool isAttacking = false;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (!isAttacking)
            {
                CheckForPlayer();
            }
        }

        private void CheckForPlayer()
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 8f, playerLayer);

            if (playerCollider != null)
            {
                Attack(playerCollider.transform.position);
            }
        }

        private void Attack(Vector2 playerPosition)
        {
            Vector2 jumpDirection = (playerPosition - (Vector2)transform.position).normalized;

            rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);

            isAttacking = true;
            Invoke(nameof(ResetAttack), attackCooldown);
        }

        private void ResetAttack()
        {
            isAttacking = false;
        }
    }
}