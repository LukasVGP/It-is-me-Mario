using UnityEngine;
using System.Collections;

public class BossHealth : MonoBehaviour
{
    private Animator anim;
    private int health = 100;
    private bool canDamage;
    private Boss boss;

    public GameObject deathEffect;
    public bool isInvulnerable = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
        boss = GetComponent<Boss>();
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        health -= damage;

        if (health <= 50)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        if (boss != null)
        {
            boss.DefeatBoss();
        }
        else
        {
            Debug.LogError("Boss component is null!");
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (canDamage)
        {
            if (target.tag == MyTags.BULLET_TAG)
            {
                TakeDamage(1);
                canDamage = false;

                if (health == 0)
                {
                    Die();
                }

                StartCoroutine(WaitForDamage());
            }
        }
    }
}
