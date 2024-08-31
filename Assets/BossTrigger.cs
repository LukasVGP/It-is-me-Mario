using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss; // Reference to the boss GameObject

    private Animator bossAnimator;
    private Rigidbody2D bossRigidbody;

    void Start()
    {
        if (boss == null)
        {
            Debug.LogError("Boss GameObject is not assigned in the Inspector.");
            return;
        }

        // Ensure the boss scripts are disabled at the start
        Boss bossComponent = boss.GetComponent<Boss>();
        BossWeapon bossWeapon = boss.GetComponent<BossWeapon>();

        if (bossComponent != null)
        {
            bossComponent.enabled = false;
        }
        else
        {
            Debug.LogError("Boss component not found on the Boss GameObject.");
        }

        if (bossWeapon != null)
        {
            bossWeapon.enabled = false;
        }
        else
        {
            Debug.LogError("BossWeapon component not found on the Boss GameObject.");
        }

        // Disable the Animator component
        bossAnimator = boss.GetComponent<Animator>();
        if (bossAnimator != null)
        {
            bossAnimator.enabled = false;
        }
        else
        {
            Debug.LogError("Animator component not found on the Boss GameObject.");
        }

        // Stop any movement by disabling the Rigidbody2D
        bossRigidbody = boss.GetComponent<Rigidbody2D>();
        if (bossRigidbody != null)
        {
            bossRigidbody.velocity = Vector2.zero; // Stop movement
            bossRigidbody.isKinematic = true; // Make it kinematic to prevent physics from affecting it
        }
        else
        {
            Debug.LogError("Rigidbody2D component not found on the Boss GameObject.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered by: " + other.name); // Debugging line

        // Check if the player enters the trigger area
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected in trigger area."); // Debugging line

            // Activate the boss components
            Boss bossComponent = boss.GetComponent<Boss>();
            BossWeapon bossWeapon = boss.GetComponent<BossWeapon>();

            if (bossComponent != null)
            {
                bossComponent.enabled = true;
                Debug.Log("Boss component enabled.");
            }
            else
            {
                Debug.LogError("Boss component is null when player enters trigger.");
            }

            if (bossWeapon != null)
            {
                bossWeapon.enabled = true;
                Debug.Log("BossWeapon component enabled.");
            }
            else
            {
                Debug.LogError("BossWeapon component is null when player enters trigger.");
            }

            // Enable the Animator component
            if (bossAnimator != null)
            {
                bossAnimator.enabled = true;
                Debug.Log("Animator component enabled.");
            }
            else
            {
                Debug.LogError("Animator component is null when player enters trigger.");
            }

            // Re-enable movement if necessary
            if (bossRigidbody != null)
            {
                bossRigidbody.isKinematic = false; // Allow physics to move the boss again
                Debug.Log("Rigidbody2D component enabled.");
            }
            else
            {
                Debug.LogError("Rigidbody2D component is null when player enters trigger.");
            }

            Debug.Log("Boss activated."); // Debugging line
        }
    }
}
