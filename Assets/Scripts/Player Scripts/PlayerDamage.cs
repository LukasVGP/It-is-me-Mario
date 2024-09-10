using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDamage : MonoBehaviour
{
    private Text lifeText;
    public int lifeScoreCount;
    private bool canDamage;
    public Transform respawnPoint;

    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        UpdateLifeText();
        canDamage = true;
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
        if (canDamage)
        {
            lifeScoreCount -= damage / 100;
            UpdateLifeText();
            CheckGameOver();
            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    private void UpdateLifeText()
    {
        lifeText.text = "x" + Mathf.Max(lifeScoreCount, 0);
    }

    private void CheckGameOver()
    {
        if (lifeScoreCount <= 0)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }
            else
            {
                Debug.LogError("GameManager instance not found!");
            }
        }
    }

    public void Respawn()
    {
        transform.position = respawnPoint.position;
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }
}
