using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] public int maxHealth;
        private int currentHealth;
        public Animator animator;
        private bool isDead = false;

        private void Start()
        {
            currentHealth = maxHealth;
            

        }

        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            isDead = true;
            animator.SetTrigger("death");

            // Disable the GameObject after the death animation finishes
            float deathAnimationDuration = GetDeathAnimationDuration();
            Invoke(nameof(DisableGameObject), deathAnimationDuration);
        }

        private void DisableGameObject()
        {
            gameObject.SetActive(false);
        }

        private float GetDeathAnimationDuration()
        {
            AnimationClip death = null; // Assign your death animation clip here
            if (death != null)
            {
                return death.length;
            }
            return 0f;
        }
    }
}