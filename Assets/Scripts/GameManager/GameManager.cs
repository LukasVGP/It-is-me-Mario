using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

        // Hide screens at start
        if (winScreen != null) winScreen.SetActive(false);
        if (gameOverScreen != null) gameOverScreen.SetActive(false);
    }

    void Update()
    {
        if (isBossDefeated && !isGameOver)
        {
            WinGame();
        }
    }

    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            if (gameOverScreen != null) gameOverScreen.SetActive(true);
            PlaySound(gameOverSound);
            Time.timeScale = 0f;
        }
    }

    public void WinGame()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            if (winScreen != null) winScreen.SetActive(true);
            PlaySound(winSound);
            Time.timeScale = 0f;
        }
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
