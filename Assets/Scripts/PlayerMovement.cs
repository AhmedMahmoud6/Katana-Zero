using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 5f;
    private float jumpingPower = 16f;
    public static bool isFacingRight = true;
  

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TrailRenderer tr;
    public Animator anim;
    float AnimationSpeed;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 6f;
    private float dashingTime = 0.3f;
    private float dashingCooldown = 1f;



    public static bool attack = false;
    public GameObject Slash;


    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        TotalDash();
        Flip();
        Jump();
        Attack();

    }
    void Movement()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        AnimationSpeed = 0;
        if (Input.GetKey(KeyCode.D))
        {
            AnimationSpeed = 0.5f;
            if (speed > 7)
            {
                AnimationSpeed = 1f;
            }
            else if (speed < 7)
            {
                speed += 0.2f;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            AnimationSpeed = 0.5f;
            if (speed > 7)
            {
                AnimationSpeed = 1f;
            }
            else if (speed < 7)
            {
                speed += 0.2f;
            }
        }

        if (Input.GetKeyUp(KeyCode.D)) { speed = 5f; }
        if (Input.GetKeyUp(KeyCode.A)) { speed = 5f; }
        anim.SetFloat("Speed", AnimationSpeed);
    }
    void FixedUpdate()
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
        anim.SetBool("Dash",true);
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
        anim.SetBool("Dash", false);
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    void TotalDash()
    {
        if (isDashing)
        {
            return;
        }
        if (Input.GetKey(KeyCode.S) && canDash && IsGrounded())
        {
            if (Input.GetKey(KeyCode.D))
            {
                StartCoroutine(Dash(true));
            }


            else if (Input.GetKey(KeyCode.A))
            {
                StartCoroutine(Dash(false));
            }
        }
    }
    void Jump()
    {
        if (IsGrounded())
        {
            anim.SetBool("Jump", false);
        }
        if (!IsGrounded())
        {
            anim.SetBool("Jump", true);
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
            anim.SetBool("Jump", true);
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && attack == false)
        {
            attack = true;
            Slash.SetActive(true);
            anim.SetTrigger("Attack");
            StartCoroutine(AttackTimer());
        }
    }
    IEnumerator AttackTimer()
    {
        yield return new WaitForSeconds(0.8f);
        PlayerMovement.attack = false;
        Slash.SetActive(false);
    }



}
