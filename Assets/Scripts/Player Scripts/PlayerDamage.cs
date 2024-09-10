using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDamage : MonoBehaviour
{
    private Text lifeText;
    public int lifeScoreCount;
    private bool canDamage;
    public Transform respawnPoint;
    private Rigidbody2D rb;
    private Animator anim;
    public float repelForce = 5f;
    public float deathJumpForce = 10f;
    private bool isDead = false;

    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        UpdateLifeText();
        canDamage = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void DealDamage()
    {
        if (canDamage && !isDead)
        {
            lifeScoreCount--;
            UpdateLifeText();

            if (lifeScoreCount > 0)
            {
                // First hit: repel
                RepelPlayer();
            }
            else
            {
                // Second hit: death
                StartCoroutine(DeathSequence());
            }

            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    public void TakeDamage(int damage)
    {
        if (canDamage && !isDead)
        {
            lifeScoreCount -= damage / 50; // Adjust this value to balance boss damage
            UpdateLifeText();

            if (lifeScoreCount > 0)
            {
                RepelPlayer();
            }
            else
            {
                StartCoroutine(DeathSequence());
            }

            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    private void RepelPlayer()
    {
        Vector2 repelDirection = transform.right * (transform.localScale.x > 0 ? -1 : 1);
        rb.velocity = Vector2.zero;
        rb.AddForce(repelDirection * repelForce, ForceMode2D.Impulse);
    }

    private IEnumerator DeathSequence()
    {
        isDead = true;
        anim.SetTrigger("Death");
        rb.velocity = Vector2.zero;
        rb.AddForce(Vector2.up * deathJumpForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(1f);

        gameObject.SetActive(false);

        yield return new WaitForSeconds(1f);

        Respawn();
    }

    private void UpdateLifeText()
    {
        lifeText.text = "x" + Mathf.Max(lifeScoreCount, 0);
    }

    public void Respawn()
    {
        isDead = false;
        transform.position = respawnPoint.position;
        gameObject.SetActive(true);
        lifeScoreCount = 3;
        UpdateLifeText();
        anim.SetTrigger("Respawn");
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }
}
