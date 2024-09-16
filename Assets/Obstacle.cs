using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private float pushForce = 10f;
    [SerializeField] private float upwardForce = 5f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            // Deal damage
            PlayerDamage playerDamage = col.GetComponent<PlayerDamage>();
            if (playerDamage != null)
            {
                playerDamage.DealDamage();
            }

            // Push player
            Rigidbody2D playerRb = col.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // Calculate direction away from the obstacle
                Vector2 pushDirection = (col.transform.position - transform.position).normalized;

                // Apply horizontal force (push back)
                playerRb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);

                // Apply upward force
                playerRb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
            }
        }
    }
}
