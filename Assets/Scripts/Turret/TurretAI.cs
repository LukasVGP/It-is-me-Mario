using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    // Int
    public int curHealth;
    public int maxHealth;
    public int damageAmount = 20;

    //Float
    public float distance;
    public float wakerange;
    public float shootinterval;
    public float bulletSpeed = 100;
    public float bulletTimer;

    //Bool
    public bool awake = false;
    public bool lookingRight = true;

    //References
    public GameObject bullet;
    public Transform target;
    public Animator anim;
    public Transform shootPointLeft, shootPointRight;

    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void Start()
    {
        curHealth = maxHealth;
    }

    void Update()
    {
        anim.SetBool("Awake", awake);
        anim.SetBool("LookingRight", lookingRight);
        RangeCheck();

        if (target.transform.position.x > transform.position.x)
        {
            lookingRight = true;
        }
        if (target.transform.position.x < transform.position.x)
        {
            lookingRight = false;
        }

        if (curHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    void RangeCheck()
    {
        distance = Vector3.Distance(transform.position, target.transform.position);

        if (distance < wakerange)
        {
            awake = true;
        }
        if (distance > wakerange)
        {
            awake = false;
        }
    }

    public void Attack(bool attackingRight)
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer >= shootinterval)
        {
            Vector2 direction = target.transform.position - transform.position;
            direction.Normalize();

            GameObject bulletClone;
            if (!attackingRight)
            {
                bulletClone = Instantiate(bullet, shootPointLeft.position, Quaternion.identity);
            }
            else
            {
                bulletClone = Instantiate(bullet, shootPointRight.position, Quaternion.identity);
            }

            bulletClone.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            bulletTimer = 0;

            Debug.Log("Turret fired a bullet!");
        }
    }


    public void TakeDamage(int damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Play death animation if you have one
        // anim.SetTrigger("Die");

        // Disable the turret's functionality
        enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // Destroy the turret after a delay (adjust as needed)
        Destroy(gameObject, 1f);
    }

}
