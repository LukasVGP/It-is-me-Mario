using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{

    private float speed = 10f;
    private Animator anim;
    private bool canMove;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        canMove = true;
        StartCoroutine(DisableBullet(5f));
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (canMove)
        {
            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;
            transform.position = temp;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }

    IEnumerator DisableBullet(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag == MyTags.BEETLE_TAG || target.gameObject.tag == MyTags.SNAIL_TAG
            || target.gameObject.tag == MyTags.SPIDER_TAG || target.gameObject.tag == MyTags.BOSS_TAG)
        {
            anim.Play("Explode");
            canMove = false;

            // Deal damage to the boss
            if (target.gameObject.tag == MyTags.BOSS_TAG)
            {
                Boss boss = target.GetComponent<Boss>();
                if (boss != null)
                {
                    boss.TakeDamage(10); // Adjust the damage value as needed
                }
            }

            StartCoroutine(DisableBullet(0.1f));
        }
    }
}











































