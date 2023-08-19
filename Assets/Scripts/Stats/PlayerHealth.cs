using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MetroidvaniaTools
{

    public class PlayerHealth : Character
    {
        [SerializeField] public float maxHealth;
        private float currentHealth;
        public Animator animator;
        public Image healthBar;
        public AudioSource hurtSFX;

        private void Start()
        {
            currentHealth = maxHealth;
            healthBar.fillAmount = currentHealth / maxHealth;
        }

        public void TakeDamage(float damageAmount)
        {
            hurtSFX.Play();
            currentHealth -= damageAmount;
            healthBar.fillAmount = currentHealth / 100f;

            if (currentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // You can put death logic here, such as game over or respawn
            SceneManager.LoadScene("MainMenu");
            Debug.Log("Player has died.");
        }
    }
}