using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetroidvaniaTools
{
    public class EnemyDamage : MonoBehaviour
    {
        [SerializeField] float damageAmount;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    Debug.Log("Player took damage from enemy.");
                }
            }
        }
    }
}