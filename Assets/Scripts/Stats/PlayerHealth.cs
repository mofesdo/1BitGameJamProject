using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{

    public class PlayerHealth : Character
    {
        [SerializeField] public int maxHealth;
        private int currentHealth;
        public Animator animator;

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
            // You can put death logic here, such as game over or respawn
            Debug.Log("Player has died.");
        }
    }
}