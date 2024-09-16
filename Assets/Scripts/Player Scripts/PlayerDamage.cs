using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerDamage : MonoBehaviour
{
    private Text lifeText;
    public int lifeScoreCount;
    private bool canDamage;
    private PlayerRespawn playerRespawn;
    public string gameOverSceneName = "GameOverScene"; // Set this in the inspector
    [SerializeField] private float invincibilityDuration = 1.5f;
    private bool isInvincible=false;

    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        UpdateLifeText();
        canDamage = true;
        playerRespawn = GetComponent<PlayerRespawn>();
    }

    void Start()
    {
        Time.timeScale = 1f;
    }

    public void DealDamage()
    {
        if (canDamage)
        {
            lifeScoreCount--;
            UpdateLifeText();
            CheckGameOver();
            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvincible)
        {
            Debug.Log("Player is invincible. Damage ignored.");
            return;
        }

        Debug.Log($"Player taking {damage} damage. Current life: {lifeScoreCount}");
        lifeScoreCount -= damage;
        UpdateLifeText();
        CheckGameOver();

        // Start invincibility
        StartCoroutine(InvincibilityCoroutine());
    }
    private void UpdateLifeText()
    {
        lifeText.text = "x" + Mathf.Max(lifeScoreCount, 0);
        Debug.Log($"Updated life text: {lifeText.text}");
    }
    private void CheckGameOver()
    {
        if (lifeScoreCount <= 0)
        {
            Debug.Log("Player health reached 0. Triggering Game Over.");
            GameOver();
        }
    }

    private void GameOver()
    {
        // Load the game over scene
        SceneManager.LoadScene(gameOverSceneName);
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        Debug.Log("Player invincibility started.");
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
        Debug.Log("Player invincibility ended.");
    }

    public void Respawn()
    {
        if (playerRespawn != null)
        {
            playerRespawn.Respawn();
        }
        else
        {
            Debug.LogError("PlayerRespawn component not found!");
        }
    }
}
