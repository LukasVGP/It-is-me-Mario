using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string nextLevel; // Name of the next level scene

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if the player is near the door
            Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, 1f, LayerMask.GetMask("Player"));
            if (playerCollider != null)
            {
                // Load the next level
                SceneManager.LoadScene(nextLevel);
            }
        }
    }
}
