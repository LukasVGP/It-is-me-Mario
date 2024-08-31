using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDamage : MonoBehaviour
{

    private Text lifeText;
    public int lifeScoreCount;

    private bool canDamage;

    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 3;
        lifeText.text = "x" + lifeScoreCount;

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
            Debug.Log("Dealing Damage");
            lifeScoreCount--;

            if (lifeScoreCount >= 0)
            {
                lifeText.text = "x" + lifeScoreCount;
            }

            if (lifeScoreCount == 0)
            {
                // RESTART THE GAME
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }

            canDamage = false;

            StartCoroutine(WaitForDamage());
        }
    }

    public void TakeDamage(int damage)
    {
        if (canDamage)
        {
            Debug.Log("Taking Damage: " + damage);
            lifeScoreCount -= damage / 100; // Assuming damage is a percentage

            if (lifeScoreCount >= 0)
            {
                lifeText.text = "x" + lifeScoreCount;
            }

            if (lifeScoreCount == 0)
            {
                // RESTART THE GAME
                Time.timeScale = 0f;
                StartCoroutine(RestartGame());
            }

            canDamage = false;

            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
        Debug.Log("Can take damage again");
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Gameplay");
    }

} // class
