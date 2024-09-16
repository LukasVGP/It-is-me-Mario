using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruzMotherHealth : MonoBehaviour
{
    public int maxHealth = 500;
    private int currentHealth;
    private Animator animator;
    private bool isDead=false;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }
        currentHealth -= damage;

        // Play hit animation if you have one
        // animator.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (isDead)
        {
            return;
        }
        isDead = true;
        // Play death animation
        animator.SetTrigger("Die");

        // Disable the GruzMother script
        GetComponent<GruzMother>().enabled = false;

        // Disable the collider
        GetComponent<Collider2D>().enabled = false;

        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
