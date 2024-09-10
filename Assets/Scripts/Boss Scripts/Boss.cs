using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public int health = 100;
    public Transform player;
    public bool isFlipped = false;
    private bool canDamage;


    public GameObject stone;
    public Transform attackInstantiate;

    public float chaseSpeed = 2.5f;
    public float attackRange = 3f;
    public int meleeDamage = 20;

    private Animator anim;
    private bool isChasing;
    private string coroutine_Name = "StartAttack";

    public GameObject deathEffect;
    public bool isInvulnerable = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isChasing = false;
        anim = GetComponent<Animator>();
        canDamage = true;
       
    }

    void Start()
    {
        StartCoroutine(coroutine_Name);
    }

    void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        LookAtPlayer();
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
       
            DefeatBoss();
        }
    }

    public void DefeatBoss()
    {
        GameManager.Instance.isBossDefeated = true;
        Die();
    }

    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        
        else
        {
            Debug.LogError("Boss component is null!");
        }
        DeactivateBossScript();
        anim.Play("BossDead");
        gameObject.SetActive(false);
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            isChasing = false;
            anim.Play("BossAttack");
           
        }
    }

  

    void BackToIdle()
    {
        anim.Play("BossIdle");
    }

    public void DeactivateBossScript()
    {
        StopCoroutine(coroutine_Name);
        enabled = false;
    }

    IEnumerator StartAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2f, 5f));

            if (Vector2.Distance(transform.position, player.position) <= attackRange)
            {
                anim.Play("BossAttack");
             
            }
            else
            {
                isChasing = true;
                anim.Play("BossChase");
            }
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

