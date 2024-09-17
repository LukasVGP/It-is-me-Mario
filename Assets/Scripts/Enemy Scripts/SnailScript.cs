using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionCheckDistance = 0.1f;
    public float directionChangeDelay = 0.5f;

    private Rigidbody2D myBody;
    private Animator anim;
    public LayerMask playerLayer;
    private bool moveLeft = true;
    private bool canMove = true;
    private bool stunned;
    private float lastDirectionChangeTime;

    public Transform left_Collision, right_Collision, top_Collision;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        lastDirectionChangeTime = Time.time;
    }

    void Update()
    {
        if (canMove)
        {
            Move();
        }
        CheckCollision();
    }

    void Move()
    {
        if (moveLeft)
        {
            myBody.velocity = new Vector2(-moveSpeed, myBody.velocity.y);
        }
        else
        {
            myBody.velocity = new Vector2(moveSpeed, myBody.velocity.y);
        }
    }

    void CheckCollision()
    {
        // Wall detection
        Vector2 direction = moveLeft ? Vector2.left : Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, collisionCheckDistance);

        if (hit.collider != null && hit.collider is BoxCollider2D)
        {
            if (Time.time - lastDirectionChangeTime > directionChangeDelay)
            {
                ChangeDirection();
            }
        }

        // Player interaction
        RaycastHit2D leftHit = Physics2D.Raycast(left_Collision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(right_Collision.position, Vector2.right, 0.1f, playerLayer);
        Collider2D topHit = Physics2D.OverlapCircle(top_Collision.position, 0.2f, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 7f);
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                    anim.Play("Stunned");
                    stunned = true;

                    if (tag == MyTags.BEETLE_TAG)
                    {
                        anim.Play("Stunned");
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    leftHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == MyTags.PLAYER_TAG)
            {
                if (!stunned)
                {
                    rightHit.collider.gameObject.GetComponent<PlayerDamage>().DealDamage();
                }
                else
                {
                    if (tag != MyTags.BEETLE_TAG)
                    {
                        myBody.velocity = new Vector2(-15f, myBody.velocity.y);
                        StartCoroutine(Dead(3f));
                    }
                }
            }
        }
    }

    void ChangeDirection()
    {
        moveLeft = !moveLeft;
        Vector3 newScale = transform.localScale;
        newScale.x *= -1;
        transform.localScale = newScale;
        lastDirectionChangeTime = Time.time;
    }

    IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == MyTags.BULLET_TAG)
        {
            if (tag == MyTags.BEETLE_TAG)
            {
                anim.Play("Stunned");
                canMove = false;
                myBody.velocity = new Vector2(0, 0);
                StartCoroutine(Dead(0.4f));
            }
            if (tag == MyTags.SNAIL_TAG)
            {
                if (!stunned)
                {
                    anim.Play("Stunned");
                    stunned = true;
                    canMove = false;
                    myBody.velocity = new Vector2(0, 0);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == MyTags.PLAYER_TAG && !stunned)
        {
            collision.gameObject.GetComponent<PlayerDamage>().DealDamage();
        }
    }
}
