using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //basic movement stuff
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;
    private bool facingRight = true;
    //jump checks
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatisGround;
    private int extraJumps;
    public int extrajumpsMax;
    //dash checks
    public bool hasDash = false;
    public float dashSpeed;
    public float dashTime;
    //glide checks
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
        if (isGrounded)
        {
            //reset jumps
            extraJumps = extrajumpsMax;
            //reset gravity
            rb.gravityScale = originalGravity;
        }
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            //diable glide
            rb.gravityScale = originalGravity;
            //jump
            rb.velocity = Vector2.up * jumpForce;
            //minus one jump
            extraJumps--;
        }else if (Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded)
        {
            //jump from ground without reducing extra jumps
            rb.velocity = Vector2.up * jumpForce;
        }
        //dash
        if(hasDash && Input.GetMouseButtonDown(0))
        {
            StartCoroutine("Dash");
        }
        //glide
        if (hasGlide && Input.GetMouseButtonDown(1) && rb.velocity.y <= 0 && !isGrounded)
        {
            //reduce gravity when falling
            rb.gravityScale /= glidePower;
        }else if(hasGlide && Input.GetMouseButtonDown(1) && rb.velocity.y > 0)
        {
            //stop all vertical movement
            rb.velocity = new Vector2(moveInput * speed, 0);
            //reduce gravity
            rb.gravityScale /= glidePower;
        }
        if (hasGlide && Input.GetMouseButtonUp(1))
        {
            //reset gravity
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
