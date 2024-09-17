using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 500;
    public GameObject deathEffect;
    public bool isInvulnerable = false;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;
        Debug.Log("Boss took " + damage + " damage. Remaining health: " + health);

        if (animator != null)
        {
            animator.SetTrigger("Hit");
        }

        if (health <= 200)
        {
            animator.SetBool("IsEnraged", true);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Boss defeated!");
        if (animator != null)
        {
            animator.SetTrigger("Die");
        }

        // Disable the boss behavior
        Boss bossBehavior = GetComponent<Boss>();
        if (bossBehavior != null)
        {
            bossBehavior.enabled = false;
        }

        // Disable the collider
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        // Delay destruction to allow for death animation
        Destroy(gameObject, 2f);
    }
}

