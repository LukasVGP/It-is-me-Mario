using UnityEngine;
using UnityEngine.UI;

public class WinManager : MonoBehaviour
{
    public Button returnToMenuButton;

    private void Start()
    {
        returnToMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void ReturnToMainMenu()
    {
        GameManager.Instance.ReturnToMainMenu();
    }
}
