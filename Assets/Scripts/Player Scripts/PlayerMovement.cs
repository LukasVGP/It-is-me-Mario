using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public Transform groundCheckPosition; // Ensure this is assigned in the inspector
    public LayerMask groundLayer;

    private Rigidbody2D myBody;
    private Animator anim;
    private bool isGrounded;
    private bool jumped;
    private float jumpPower = 12f;
    private float jumpCooldown = 0.9f; // Cooldown period for jump
    private float lastJumpTime; // Time when the player last jumped

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        PlayerJump();
    }

    void FixedUpdate()
    {
        CheckIfGrounded();
        PlayerWalk();
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (h > 0)
        {
            myBody.velocity = new Vector2(speed, myBody.velocity.y);
            ChangeDirection(1);
        }
        else if (h < 0)
        {
            myBody.velocity = new Vector2(-speed, myBody.velocity.y);
            ChangeDirection(-1);
        }
        else
        {
            myBody.velocity = new Vector2(0f, myBody.velocity.y);
        }

        anim.SetInteger("Speed", Mathf.Abs((int)myBody.velocity.x));
    }

    void ChangeDirection(int direction)
    {
        Vector3 tempScale = transform.localScale;
        tempScale.x = direction;
        transform.localScale = tempScale;
    }

    void CheckIfGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPosition.position, 0.1f, groundLayer);

        if (isGrounded)
        {
            if (jumped)
            {
                jumped = false;
                anim.SetBool("Jump", false);
            }
        }
    }

    void PlayerJump()
    {
        if (isGrounded && Input.GetButtonDown("Jump") && Time.time > lastJumpTime + jumpCooldown)
        {
            jumped = true;
            lastJumpTime = Time.time;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpPower);
            anim.SetBool("Jump", true);
        }
    }
}
