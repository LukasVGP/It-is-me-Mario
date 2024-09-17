using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{

    private float speed = 10f;
    private Animator anim;
    private bool canMove;
    public int bulletDamage = 50;

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
            || target.gameObject.tag == MyTags.SPIDER_TAG || target.gameObject.tag == "GruzMother" || target.gameObject.tag == MyTags.BOSS_TAG)
        {
            anim.Play("Explode");
            canMove = false;

            if (target.gameObject.tag == "GruzMother")
            {
                GruzMother gruzMother = target.GetComponent<GruzMother>();
                if (gruzMother != null)
                {
                    gruzMother.TakeDamage(bulletDamage);
                }
            }
            else if (target.gameObject.tag == MyTags.BOSS_TAG)
            {
                BossHealth bossHealth = target.GetComponent<BossHealth>();
                if (bossHealth != null)
                {
                    bossHealth.TakeDamage(bulletDamage);
                }
            }

            StartCoroutine(DisableBullet(0.1f));
        }
    }

}












































