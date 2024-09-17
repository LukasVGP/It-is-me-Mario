using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Level_transition : MonoBehaviour
{
    public string nextLevelName; // Set this in the Inspector to the name of the next scene

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LevelEnd"))
        {
            if (GameManager.Instance != null)
            {
                if (SceneManager.GetActiveScene().name == "Level 3") // Adjust this to your final level's name
                {
                    GameManager.Instance.WinGame();
                }
                else
                {
                    LoadNextLevel();
                }
            }
            else
            {
                Debug.LogError("GameManager instance not found!");
            }
        }
    }

    private void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextLevelName))
        {
            SceneManager.LoadScene(nextLevelName);
        }
        else
        {
            Debug.LogWarning("Next level name not set!");
        }
    }
}
