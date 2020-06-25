using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;
    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatisGround;
    private int extraJumps;
    public int extrajumpsMax;

    public bool hasDash = false;
    public float dashSpeed;
    public float dashTime;

    public bool hasGlide = false;
    public float glidePower;
    public float originalGravity;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatisGround);


        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        Debug.Log(rb.velocity);

        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

     void Update()
    {
        //jump
        if (isGrounded == true)
        {
            extraJumps = extrajumpsMax;
        }
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }
        //dash
        if(hasDash == true && Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Dash");
        }
        //glide
        if(hasGlide == true && Input.GetMouseButtonDown(1) && rb.velocity.y <= 0)
        {
            rb.gravityScale /= glidePower;
        }else if(hasGlide == true && Input.GetMouseButtonDown(1) && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(moveInput * speed, 0);
            rb.gravityScale /= glidePower;
        }
        if (hasGlide == true && Input.GetMouseButtonUp(1))
        {
            rb.gravityScale = originalGravity;
        }
    }

    IEnumerator Dash()
    {
        speed += dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed -= dashSpeed;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
