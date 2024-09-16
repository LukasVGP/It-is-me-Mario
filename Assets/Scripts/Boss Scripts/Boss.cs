using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{
    public int health = 100;
    public Transform player;
    public bool isFlipped = false;
    public GameObject stone;
    public Transform attackInstantiate;
    public float chaseSpeed = 2.5f;
    public float attackRange = 3f;
    public int meleeDamage = 20;
    private Animator anim;
    private bool isChasing;
    private string coroutine_Name = "StartAttack";

    void Awake()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        isChasing = false;
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
        health -= damage;
        if (health <= 0)
        {
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
            isFlipped = true;
        }
        else if (transform.position.x < player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
    }

    public void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            isChasing = false;
            anim.Play("BossAttack");
            MeleeAttack();
        }
    }

    void Attack()
    {
        GameObject obj = Instantiate(stone, attackInstantiate.position, Quaternion.identity);
        obj.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-300f, -700), 0f));
    }

    void MeleeAttack()
    {
        anim.ResetTrigger("MeleeAttack");
        player.GetComponent<PlayerDamage>().TakeDamage(meleeDamage);
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
                Attack();
            }
            else
            {
                isChasing = true;
                anim.Play("BossChase");
            }
        }
    }
}
