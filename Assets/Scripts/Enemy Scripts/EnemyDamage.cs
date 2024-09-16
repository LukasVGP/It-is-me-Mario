using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public float pushBackForce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerDamage playerDamage = collision.gameObject.GetComponent<PlayerDamage>();
            if (playerDamage != null)
            {
                playerDamage.DealDamage();
                PushBackPlayer(collision.transform);
            }
        }
    }

    private void PushBackPlayer(Transform player)
    {
        Vector2 pushDirection = (player.position - transform.position).normalized;
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            playerRb.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        }
    }
}
