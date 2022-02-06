using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animatotor;
    private SpriteRenderer skin;

    bool facingRight;
    public float Speed = 10f;
    public float jumpForce = 30f;
    private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundlayer;
    public float checkRadius;


    //WALLJUMP
    private bool isTouchingFront;
    public Transform frontCheck;
    private bool wallSliding;
    public float wallSlidingSpeed;
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animatotor = GetComponent<Animator>();
        skin = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input *Speed, rb.velocity.y);

        if (input > 0 && facingRight == true)
        {
            skinFlip();
        } else if (input < 0 && facingRight == false) {
            skinFlip();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded == true)
        {
                Jump();
        }

        if (isTouchingFront == true && isGrounded == false)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }
  
        if (wallSliding == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, wallSlidingSpeed, float.MaxValue));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && wallSliding == true)
        {
            wallJumping = true;
            Invoke("wallJumpingToFalse", wallJumpTime);
        }

        if (wallJumping == true)
        {
            if (facingRight == false)
            {
                rb.velocity = new Vector2(-xWallForce, yWallForce);
            } else if (facingRight == true)
            {
                rb.velocity = new Vector2(xWallForce, yWallForce);
            }
        }

        animatotor.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        animatotor.SetFloat("velocityY", (rb.velocity.y));
        animatotor.SetBool("isJumping", !isGrounded);
        animatotor.SetBool("WallSliding", wallSliding);
    }

    void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
    }

    void wallJumpingToFalse()
    {
        wallJumping = false;
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundlayer);
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, groundlayer);
    }

    // HEHEHEHEHEHEHE
    void skinFlip()
    {
        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        facingRight = !facingRight;
    }
}
