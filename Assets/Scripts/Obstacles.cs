using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public int damageAmount = 10;
    public float pushForce = 10f;
    public float upwardForce = 5f;
    public bool continuousDamage = false;
    public float damageInterval = 1f;

    private float lastDamageTime;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDamage playerDamage = collision.gameObject.GetComponent<PlayerDamage>();
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerDamage != null && playerRb != null)
            {
                if (continuousDamage)
                {
                    if (Time.time - lastDamageTime >= damageInterval)
                    {
                        ApplyDamageAndForce(playerDamage, playerRb, collision.GetContact(0).normal);
                        lastDamageTime = Time.time;
                    }
                }
                else
                {
                    ApplyDamageAndForce(playerDamage, playerRb, collision.GetContact(0).normal);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDamage playerDamage = collision.GetComponent<PlayerDamage>();
            Rigidbody2D playerRb = collision.GetComponent<Rigidbody2D>();

            if (playerDamage != null && playerRb != null)
            {
                if (continuousDamage)
                {
                    if (Time.time - lastDamageTime >= damageInterval)
                    {
                        Vector2 direction = (collision.transform.position - transform.position).normalized;
                        ApplyDamageAndForce(playerDamage, playerRb, direction);
                        lastDamageTime = Time.time;
                    }
                }
                else
                {
                    Vector2 direction = (collision.transform.position - transform.position).normalized;
                    ApplyDamageAndForce(playerDamage, playerRb, direction);
                }
            }
        }
    }

    private void ApplyDamageAndForce(PlayerDamage playerDamage, Rigidbody2D playerRb, Vector2 direction)
    {
        playerDamage.TakeDamage(damageAmount);

        Vector2 pushDirection = new Vector2(direction.x, 0).normalized;
        Vector2 force = (pushDirection * pushForce) + (Vector2.up * upwardForce);
        playerRb.velocity = Vector2.zero;
        playerRb.AddForce(force, ForceMode2D.Impulse);
    }
}
