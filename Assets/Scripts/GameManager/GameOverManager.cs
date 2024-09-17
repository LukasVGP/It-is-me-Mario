using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    public float displayDuration = 5f;
    public string mainMenuSceneName = "MainMenu";
    public Button returnToMenuButton;

    void Start()
    {
        Debug.Log("GameOverManager: Start method called");
        if (returnToMenuButton != null)
        {
            returnToMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
        else
        {
            Debug.LogError("Return to Menu button not assigned!");
        }
    }

    void ReturnToMainMenu()
    {
        Debug.Log("GameOverManager: Attempting to load main menu scene: " + mainMenuSceneName);
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
