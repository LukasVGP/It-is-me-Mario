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
        if (isInvincible) return;

        // Apply damage
        lifeScoreCount -= damage;
        UpdateLifeText();
        CheckGameOver();

        // Start invincibility
        StartCoroutine(InvincibilityCoroutine());
    }


    private void UpdateLifeText()
    {
        lifeText.text = "x" + Mathf.Max(lifeScoreCount, 0);
    }

    private void CheckGameOver()
    {
        if (lifeScoreCount <= 0)
        {
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
        yield return new WaitForSeconds(invincibilityDuration);
        isInvincible = false;
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
