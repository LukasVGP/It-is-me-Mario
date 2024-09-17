using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinSceneManager : MonoBehaviour
{
    public Text scoreText;
    public Button mainMenuButton;

    void Start()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + PlayerPrefs.GetInt("Score", 0);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);
        }
    }

    public void OnMainMenuButtonClick()
    {
        ReturnToMainMenu();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(GameManager.Instance.mainMenuSceneName);
    }
}
