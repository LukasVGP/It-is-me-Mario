using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered KillZone: " + other.name); // Log when something enters the KillZone

        if (other.CompareTag("Player"))
        {
            PlayerDamage playerDamage = other.GetComponent<PlayerDamage>();
            if (playerDamage != null)
            {
                Debug.Log("PlayerDamage component found. Dealing damage and respawning.");
                playerDamage.DealDamage();
                playerDamage.Respawn();
            }
            else
            {
                Debug.LogError("PlayerDamage component not found on the player.");
            }
        }
    }
}
