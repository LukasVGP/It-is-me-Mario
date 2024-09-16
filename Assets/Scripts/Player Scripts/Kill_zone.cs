using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerDamage playerDamage = other.GetComponent<PlayerDamage>();
            if (playerDamage != null)
            {
                playerDamage.KillZoneDeath();
            }
            else
            {
                Debug.LogError("PlayerDamage component not found on the player.");
            }
        }
    }
}
