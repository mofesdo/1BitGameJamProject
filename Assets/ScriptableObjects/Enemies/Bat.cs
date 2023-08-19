using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class BatEnemy : MonoBehaviour
    {
        public float swoopSpeed = 5f;
        public float attackCooldown = 2f;
        public LayerMask playerLayer;  // The layer(s) representing the player

        private bool isAttacking = false;
        private Vector2 targetPosition;

        private void Update()
        {
            if (!isAttacking)
            {
                CheckForPlayer();
            }
            else
            {
                Swoop();
            }
        }

        private void CheckForPlayer()
        {
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 5f, playerLayer);

            if (playerCollider != null)
            {
                Attack(playerCollider.transform.position);
            }
        }

        private void Attack(Vector2 playerPosition)
        {
            targetPosition = playerPosition;
            isAttacking = true;
        }

        private void Swoop()
        {
            Vector2 newPosition = Vector2.MoveTowards(transform.position, targetPosition, swoopSpeed * Time.deltaTime);
            transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);

            if ((Vector2)transform.position == targetPosition)
            {
                isAttacking = false;
                Invoke(nameof(ResetAttack), attackCooldown);
            }
        }

        private void ResetAttack()
        {
            isAttacking = false;
        }
    }
}