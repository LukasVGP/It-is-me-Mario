using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Button restartButton;
    public Button mainMenuButton;
    public Button quitButton;

    void Start()
    {
        restartButton.onClick.AddListener(GameManager.Instance.RestartGame);
        mainMenuButton.onClick.AddListener(GameManager.Instance.ReturnToMainMenu);
        quitButton.onClick.AddListener(GameManager.Instance.QuitGame);
    }
}
