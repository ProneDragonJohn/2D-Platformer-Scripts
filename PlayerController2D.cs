using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;

    [SerializeField] GameObject attackHitBox;

    bool isGrounded;
    bool isAttacking;
    bool isJumping;
    Vector2 counterJumpForce;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform groundCheckL;
    [SerializeField] Transform groundCheckR;

    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpTime;
    [SerializeField] private float jumpTimeCounter;

    // Initialization
    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        jumpTimeCounter = jumpTime;
        attackHitBox.SetActive(false);
    }

    void Update()
    {
        if (isGrounded)
        {
            if (!isAttacking && Input.GetAxis("Horizontal") == 0 && !isJumping)
            {
                animator.Play("Player2Idle");
            }

            jumpTimeCounter = jumpTime;
        }

        if (Input.GetButtonDown("Attack") && !isAttacking)
        {
            isAttacking = true;
            animator.Play("Player2Attack");

            StartCoroutine(Attack());
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                isJumping = true;
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                animator.Play("Player2Jump");
            }
        }

        if (Input.GetButton("Jump") && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            jumpTimeCounter = 0;
            isJumping = false;
        }
    }

    private void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) ||
            Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;

            if (rb2d.velocity.y < 0 && !isAttacking)
            {
                animator.Play("Player2Fall");
            }
            else if (rb2d.velocity.y < 0 && isAttacking)
            {
                animator.Play("Player2Attack");
            }
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            rb2d.velocity = new Vector2(runSpeed, rb2d.velocity.y);

            if(isGrounded && !isAttacking)
                animator.Play("Player2Run");
            
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            rb2d.velocity = new Vector2(-runSpeed, rb2d.velocity.y);

            if (isGrounded && !isAttacking)
                animator.Play("Player2Run");
            
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        

        
    }

    IEnumerator Attack()
    {
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.2f);
        attackHitBox.SetActive(false);
        isAttacking = false;
    }
}
