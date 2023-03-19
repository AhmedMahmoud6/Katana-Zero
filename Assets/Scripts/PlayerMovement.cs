using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 5f;
    private float jumpingPower = 16f;
    private bool isFacingRight = true;


    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;


    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 16f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;



    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }




        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x,jumpingPower);
        }
        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        Flip();
        if (Input.GetKey(KeyCode.S) && canDash && IsGrounded())
        {
            if(Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(true));
            }


            else if(Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(false));
            }
        }

    }

    void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if (Input.GetKey(KeyCode.D))
        {
            if (speed > 7)
            {
                speed = 5;
            }
            else if (speed < 7)
            {
                speed += 0.2f;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (speed > 7)
            {
                speed = 5;
            }
            else if (speed < 7)
            {
                speed += 0.2f;
            }
        }

        if (Input.GetKeyUp(KeyCode.D)) { speed = 5f; }
        if (Input.GetKeyUp(KeyCode.A)) { speed = 5f; }

    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        Movement();
    }


    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }


    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

    }





    private IEnumerator Dash(bool isRight)
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        if(isRight)
        {
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        }
        if(!isRight)
        {
            rb.velocity = new Vector2(-transform.localScale.x * -dashingPower, 0f);
        }
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

}
