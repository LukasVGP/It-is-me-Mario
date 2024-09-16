using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Button restartButton;
    public Button mainMenuButton;
    public Button quitButton;
    public Canvas gameOverCanvas;

    void Start()
    {
        restartButton.onClick.AddListener(RestartGame);
        mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        quitButton.onClick.AddListener(QuitGame);
        gameOverCanvas = GetComponent<Canvas>();
        gameOverCanvas.enabled = false;
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        gameOverCanvas.enabled = false;
        GameManager.Instance.RestartGame();
    }

    void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ReturnToMainMenu();
    }

    void QuitGame()
    {
        GameManager.Instance.QuitGame();
    }
}
