using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public float pushBackForce = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Collision with Player");
            PushBackPlayer(collision.transform);
        }
    }

    private void PushBackPlayer(Transform player)
    {
        Vector2 pushDirection = (player.position - transform.position).normalized;
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        if (playerRb != null)
        {
            Debug.Log("Pushing back player");
            playerRb.AddForce(pushDirection * pushBackForce, ForceMode2D.Impulse);
        }
    }
}
