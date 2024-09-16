using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_zone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDamage playerDamage = collision.GetComponent<PlayerDamage>();
            if (playerDamage != null)
            {
                playerDamage.DealDamage();
                playerDamage.Respawn();
            }
            else
            {
                Debug.LogError("PlayerDamage component not found on the player!");
            }
        }
    }
}
