using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

	public GameObject stone;
	public Transform attackInstantiate;

	public float chaseSpeed;
	public float attackRange;
	public int meleeDamage;

	private Transform player;
	private bool isChasing;

	private Animator anim;

	private string coroutine_Name = "StartAttack";

	void Awake() {
		anim = GetComponent<Animator> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		isChasing=false;
	}

	void Start () {
		StartCoroutine (coroutine_Name);
	}

	void Attack() {
		GameObject obj = Instantiate (stone, attackInstantiate.position, Quaternion.identity);
		obj.GetComponent<Rigidbody2D> ().AddForce (new Vector2(Random.Range(-300f, -700), 0f));
	}

	void BackToIdle() {
		anim.Play ("BossIdle");
	}

	public void DeactivateBossScript() {
		StopCoroutine (coroutine_Name);
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

	void Update ()
	{
		if (isChasing)
		{
            ChasePlayer ();
        }
		LookAtPlayer();
	}

    void LookAtPlayer()
    {
        Vector3 direction = player.position - transform.position;
        Vector3 scale = transform.localScale;

        if (direction.x >= 0.1f)
        {
            scale.x = Mathf.Abs(scale.x); // Ensure the boss faces right
        }
        else if (direction.x <= -0.1f)
        {
            scale.x = -Mathf.Abs(scale.x); // Ensure the boss faces left
        }

        transform.localScale = scale;
    }


    void ChasePlayer()
	{
        transform.position = Vector2.MoveTowards(transform.position, player.position, chaseSpeed * Time.deltaTime);

		if (Vector2.Distance(transform.position, player.position) <= attackRange)
		{
			isChasing = false;
			anim.Play("BossAttack");
			MeleeAttack();
		}
    }

	void MeleeAttack()
	{
		anim.ResetTrigger("MeleeAttack");
		player.GetComponent<PlayerDamage>().TakeDamage(meleeDamage);
	}

} // class

































