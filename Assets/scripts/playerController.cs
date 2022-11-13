using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalMove = 0f;
    private bool facingRight = true;
    public Animator animator;
    [Header("Player Settings")]
    [Range(0, 15f)] public float speed = 1f;
    public float jumpForce = 8f;

    [Space]
    [Header("Ground Check Settings")]
    public bool isGrounded = false;
    [Range(-2f, 2f)] public float checkGroundOffsetY = -1.1f;
    [Range(0f, 5f)] public float checkGroundRadius = 0.3f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
        animator.SetFloat("horizontal", Mathf.Abs(horizontalMove));
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
        if (horizontalMove < 0 && facingRight)
        {
            Flip();
        }
        else if (horizontalMove > 0 && !facingRight)
        {
            Flip();
        }
        if (isGrounded == false)
        {
            animator.SetBool("Jump", true);
        }

        {
            animator.SetBool("Jump", false);
        }

    }
    private void FixedUpdate()
    {
        Vector2 targetVelocity = new Vector2(horizontalMove * 5f, rb.velocity.y);
        rb.velocity = targetVelocity;

        CheckGround();
    }
    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }
    private void CheckGround()
    {     
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y + checkGroundOffsetY), checkGroundRadius);
        if (colliders.Length > 1)
        { 
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}
