using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnvironmentDamage : MonoBehaviour
{
    private Text lifeText;
    private int lifeScoreCount;
    private bool canDamage;

    // Reference to the player's movement script
    private PlayerMovement playerMovement;

    // The respawn point
    private Vector3 respawnPoint;

    void Awake()
    {
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        lifeScoreCount = 100; // Initial life force
        lifeText.text = "Life: " + lifeScoreCount;

        canDamage = true;

        // Get the player movement script
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        Time.timeScale = 1f;
        // Set the respawn point to the player's initial position
        respawnPoint = playerMovement.transform.position;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Hazard") && canDamage)
        {
            DealDamage(collision);
        }
    }

    public void DealDamage(Collision2D collision)
    {
        if (canDamage)
        {
            lifeScoreCount -= 50; // Reduce life force by 50

            if (lifeScoreCount > 0)
            {
                lifeText.text = "Life: " + lifeScoreCount;
                // Push the player back
                Vector2 pushDirection = (transform.position - collision.transform.position).normalized;
                playerMovement.GetComponent<Rigidbody2D>().AddForce(pushDirection * 500f); // Adjust the force as needed
            }
            else
            {
                // Player dies
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
            }

            canDamage = false;
            StartCoroutine(WaitForDamage());
        }
    }

    IEnumerator WaitForDamage()
    {
        yield return new WaitForSeconds(2f);
        canDamage = true;
    }
}
