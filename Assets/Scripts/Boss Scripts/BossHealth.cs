using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{

    private Animator anim;
    private int health = 100;
    private bool canDamage;
    private Boss boss; // Reference to the Boss script

    public GameObject deathEffect;
    public bool isInvulnerable = false;

    public void takeDamage(int damage)
    {
        if (!isInvulnerable) return;

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
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void Awake()
    {
        anim = GetComponent<Animator>();
        canDamage = true;
        boss = GetComponent<Boss>(); // Initialize the Boss reference
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
                health--;
                canDamage = false;

                if (health == 0)
                {
                    GetComponent<BossScript>().DeactivateBossScript();
                    anim.Play("BossDead");

                    // Call the DefeatBoss method when the boss is defeated
                    if (boss != null)
                    {
                        boss.DefeatBoss();
                    }
                }

                StartCoroutine(WaitForDamage());
            }
        }
    }
}


































