using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public int damage = 10;
    public float repelForce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDamage playerDamage = collision.gameObject.GetComponent<PlayerDamage>();
            if (playerDamage != null)
            {
                playerDamage.TakeDamage(damage);
                RepelPlayer(collision.transform);
            }
        }
    }

    private void RepelPlayer(Transform player)
    {
        Vector2 repelDirection = (player.position - transform.position).normalized;
        player.GetComponent<Rigidbody2D>().AddForce(repelDirection * repelForce, ForceMode2D.Impulse);
    }
}
