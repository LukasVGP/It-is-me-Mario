using UnityEngine;

public class Stone : MonoBehaviour
{
    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDamage playerDamage = collision.GetComponent<PlayerDamage>();
            if (playerDamage != null)
            {
                playerDamage.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Destroy(gameObject, 5f); // Destroy the stone after 5 seconds if it doesn't hit anything
    }
}
