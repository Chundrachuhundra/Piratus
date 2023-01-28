using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private float horizontalMove = 0f;
    private bool facingRight = true;
    public Animator animator;
    [Header("Player Settings")]
    [Range(0, 15f)] public float speed = 1f;
    public float jumpForce = 8f;
    /*float SX, SY;*/
    private Vector3 respawn;    
   
    [Space]
    [Header("Ground Check Settings")]
    public bool isGrounded = false;
    [Range(-2f, 2f)] public float checkGroundOffsetY = -1.1f;
    [Range(0f, 5f)] public float checkGroundRadius = 0.3f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        /*SX = transform.position.x;
        SY = transform.position.y;*/
        respawn = transform.position;
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
            animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool("jump", false);
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
        transform.Rotate(0f, 180f, 0f);
     }
        private void OnTriggerEnter2D(Collider2D collision)
        {
/*            if (collision.gameObject.name == "DeadZone")
            {
                transform.position = new Vector3(SX,SY, transform.position.z);*/
            if(collision.gameObject.name == "DeadZone")
            {
                transform.position = respawn;
            }else if(collision.tag == "CheckPoint")
            {
                respawn = transform.position;
            }
        }
        if (collision.gameObject.name == "Next")
        {
            SceneManager.LoadScene("level2");
        }
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("platform"))
        {
            this.transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("platform"))
        {
            this.transform.parent = null;
        }
    }
}
