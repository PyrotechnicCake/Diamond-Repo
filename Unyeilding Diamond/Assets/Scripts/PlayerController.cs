using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator anim;
    
    //basic movement stuff
    private float speed;
    public float jumpForce;
    private float moveInput;
    public float sprintTimer;
    public float time;
    public float sprintSpeed = 2;
    public float walkSpeed = 5;
    private bool isSprinting = false;
    public float falling;

    private Rigidbody2D rb;
    private bool facingRight = true;
    //jump checks
    public bool hasJump = false;
    public bool isGrounded;
    public Transform groundCheck;
    private float checkRadius = 0.5f;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extrajumpsMax;
    public bool isJumping;
    //dash checks
    public bool hasDash = false;
    public bool dashReady = true;
    public float dashRefreshTime;
    public float dashSpeed;
    public float dashTime;
    //glide checks
    public bool hasGlide = false;
    public float glidePower;
    private float originalGravity;
    //fly checks
    public bool isGliding = false;
    public bool hasFly = false;
    public float flySpeed;
    private float vertMoveInput;

    //gameplay checks
    public int deathCount = 0;
    public int checkpointNum = 0;
    public GameObject myLastCheckpoint;
    public GameManager gm;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        hasJump = gm.hasJump;
        hasDash = gm.hasDash;
        hasGlide = gm.hasGlide;
        extrajumpsMax = gm.extraJumps;
        myLastCheckpoint = GameObject.FindGameObjectWithTag(gm.lastCheckpoint);
        gameObject.transform.position = myLastCheckpoint.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


        moveInput = Input.GetAxisRaw("Horizontal");
        vertMoveInput = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
        anim.SetFloat("Moveinput", Mathf.Abs(moveInput)); // inorder to detect movement we pass the moveinput as a float to the animator which has condition where if moveinput is greater than 0 it would play the animation.
        if(facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
        if(moveInput != 0 && !isSprinting && isGrounded)
        {
            Sprint();
        }
        if (moveInput == 0)
        {
            speed = walkSpeed;
            time = 0;
            isSprinting = false;
            //Debug.Log("stop sprinting");
        }
    }

     void Update()
    {
        if(isGrounded == false && falling > rb.position.y)
        {
            anim.SetBool("Falling", true);
        }
        //jump
        if (isGrounded)
        {
            anim.SetBool("Falling", false);
            //reset jumps
            extraJumps = extrajumpsMax;
            //reset gravity
            rb.gravityScale = originalGravity;
            anim.SetBool("Jump", false);
            anim.SetBool("Gliding", false);
        }
        else
        {
            anim.SetBool("Jump", true);
        }
        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            //diable glide
            rb.gravityScale = originalGravity;
            anim.SetTrigger("Takeoff");
            //jump
            rb.velocity = Vector2.up * jumpForce;
            //minus one jump
            extraJumps--;
        }else if (hasJump && Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded)
        {
            //jump from ground without reducing extra jumps
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("Takeoff");
        }
        //dash
        if(hasDash && dashReady && Input.GetButtonDown("Dash"))
        {
            StartCoroutine("Dash");
        }
        //glide
        if (hasGlide && Input.GetButtonDown("Glide") && rb.velocity.y <= 0 && !isGrounded)
        {
            anim.SetBool("Gliding", true);
            //stop all vertical movement
            rb.velocity = new Vector2(moveInput * speed, 0);
            //reduce gravity when falling
            //rb.gravityScale /= glidePower;
            rb.gravityScale = 0.25f;
            isGliding = true;
        }
        else if(hasGlide && Input.GetButtonDown("Glide") && rb.velocity.y > 0)
        {
            anim.SetBool("Gliding", true);
            //stop all vertical movement
            rb.velocity = new Vector2(moveInput * speed, 0);
            //reduce gravity
            //rb.gravityScale /= glidePower;
            rb.gravityScale = 0.25f;
            isGliding = true;
        }
        if (hasGlide && Input.GetButtonUp("Glide"))
        {
            anim.SetBool("Gliding", false);
            //reset gravity
            rb.gravityScale = originalGravity;
            isGliding = false;
        }
        if(isGliding && hasFly && Input.GetAxisRaw("Vertical") > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, vertMoveInput * flySpeed);
        }
        falling = rb.position.y;
    }

    IEnumerator Dash()
    {
        speed += dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed -= dashSpeed;
        dashReady = false;
        yield return new WaitForSeconds(dashRefreshTime);
        dashReady = true;
    }
    void Sprint()
    {
        time += Time.deltaTime;
        if(time >= sprintTimer)
        {
            speed += sprintSpeed;
            isSprinting = true;
            //Debug.Log("Sprinting!");
        }
        
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MovingPlatform")
        {
            gameObject.transform.parent = collision.transform;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            gameObject.transform.parent = null;
        }
    }
}
