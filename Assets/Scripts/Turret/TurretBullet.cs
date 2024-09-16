using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBullet : MonoBehaviour
{
    public int damageAmount = 1; // Set this in the inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true)
        {
            if (collision.CompareTag("Player"))
            {
                PlayerDamage playerDamage = collision.GetComponent<PlayerDamage>();
                if (playerDamage != null)
                {
                    Debug.Log("Bullet hit player. Attempting to deal " + damageAmount + " damage.");
                    playerDamage.TakeDamage(damageAmount);
                }
                else
                {
                    Debug.LogError("PlayerDamage component not found on player!");
                }
                DestroyBullet();
            }
            else if (collision.CompareTag("Ground"))
            {
                Debug.Log("Bullet hit ground.");
                DestroyBullet();
            }
            else
            {
                Debug.Log("Bullet hit something else: " + collision.gameObject.name);
                DestroyBullet();
            }
        }
    }

    private void DestroyBullet()
    {
        Debug.Log("Destroying bullet.");
        Destroy(gameObject);
    }

    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}
