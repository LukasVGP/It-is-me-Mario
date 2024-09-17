using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public bool isBossDefeated = false;
    public bool isGameOver = false;
    public GameObject winScreen;
    public GameObject gameOverScreen;
    public AudioClip winSound;
    public AudioClip gameOverSound;
    public string mainMenuSceneName = "MainMenu";
    public string gameOverSceneName = "GameOverScene";
    public string winSceneName = "WinScene";
    public float delayBeforeGameOverScene = 2f;

    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            PlaySound(gameOverSound);
            StartCoroutine(GameOverSequence());
        }
    }

    private IEnumerator GameOverSequence()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(delayBeforeGameOverScene);
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void WinGame()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            PlaySound(winSound);
            StartCoroutine(WinSequence());
        }
    }

    private IEnumerator WinSequence()
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(winSceneName);
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        isBossDefeated = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        isGameOver = false;
        isBossDefeated = false;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
