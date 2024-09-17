using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int health = 500;
    public GameObject deathEffect;
    public bool isInvulnerable = false;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        if (health <= 200)
        {
            EnterEnragedState();
        }

        if (health <= 0)
        {
            Die();
        }
    }

    private void EnterEnragedState()
    {
        anim.SetBool("IsEnraged", true);

    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(MyTags.BULLET_TAG))
        {
            FireBullet bullet = other.GetComponent<FireBullet>();
            if (bullet != null)
            {
                TakeDamage(10); // Adjust this value as needed
                bullet.gameObject.SetActive(false);
            }
        }
    }
}
