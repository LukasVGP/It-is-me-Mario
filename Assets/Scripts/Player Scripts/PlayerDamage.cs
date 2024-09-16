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
    private bool isDead = false;
    private SpriteRenderer spriteRenderer;
    public int maxHealth = 100;
    private int currentHealth;

    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        UpdateLifeText();
        canDamage = true;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = maxHealth;
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void DealDamage()
    {
        if (canDamage && !isDead)
        {
            currentHealth -= 35;
            UpdateLifeText();
            if (currentHealth > 0)
            {
                RepelPlayer();
                StartCoroutine(BlinkEffect());
            }
            else
            {
                Die();
            }
            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    public void TakeDamage(int damage)
    {
        if (canDamage && !isDead)
        {
            currentHealth -= damage;
            UpdateLifeText();
            if (currentHealth > 0)
            {
                RepelPlayer();
                StartCoroutine(BlinkEffect());
            }
            else
            {
                Die();
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

    private void Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        gameObject.SetActive(false);
        Respawn();
    }

    private void UpdateLifeText()
    {
        lifeText.text = "x" + Mathf.Max(lifeScoreCount, 0) + " HP: " + currentHealth;
    }

    public void Respawn()
    {
        lifeScoreCount--;
        if (lifeScoreCount < 0)
        {
            GameManager.Instance.GameOver();
        }
        else
        {
            isDead = false;
            transform.position = respawnPoint.position;
            gameObject.SetActive(true);
            currentHealth = maxHealth;
            UpdateLifeText();
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    IEnumerator BlinkEffect()
    {
        for (int i = 0; i < 10; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.enabled = true;
    }

    public void KillZoneDeath()
    {
        Die();
    }
}
