using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public int Death = 0;

    private Rigidbody2D rb;
    private bool facingRight = true;
    //jump checks
    public bool hasJump = false;
    public bool isGrounded;
    public Transform groundCheck;
    private float checkRadius = 1f;
    public LayerMask whatIsGround;
    private int extraJumps;
    public int extrajumpsMax;
    public bool isJumping;
    public float fallMultiplier = 5f;
    public float lowJumpMultiplier = 10f;
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

    //paticles
    public ParticleSystem myDash;
    public ParticleSystem myDust;
    public ParticleSystem myJump;

    //sound
    public AudioSource powerSounds;
    public AudioSource walkingSounds;
    public AudioClip walkClip;
    public AudioClip runClip;
    public AudioClip jumpClip;
    public AudioClip jumpClipTwo;
    public AudioClip glideClip;
    public AudioClip landingClip;
    public AudioClip dashClip;
    private float lowPitch = .85f;
    private float highPitch = 1.05f;
    public bool walking = false;
    public bool landed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        myDash.Stop();
        myDash.Clear();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalGravity = rb.gravityScale;
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        gm.pc = this;
        gm.player = gameObject;
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
            walking = false;
            //Debug.Log("stop sprinting");
        }
        if(isGrounded)
        {
            anim.SetBool("Falling", false);
            anim.SetBool("Gliding", false);
        }
        if (!isGrounded)
        {
            landed = false;
        }
    }

     void Update()
    {
        if(!isGrounded && falling > rb.position.y && gameObject.transform.parent == null)
        {
            anim.SetBool("Falling", true);
            walkingSounds.Stop();
        }
        
        if(isGrounded && !landed)
        {
            walkingSounds.PlayOneShot(landingClip);
            walkingSounds.Play();
            landed = true;
        }

        //walk run particle control
        if (isGrounded) {myDust.enableEmission = true;}
        else {myDust.enableEmission = false;}

        if(Input.GetAxis("Horizontal") != 0 && isGrounded && !walking && !isSprinting)
        {
            walkingSounds.clip = walkClip;
            walkingSounds.Play();
            walking = true;
        }else if (Input.GetAxis("Horizontal") != 0 && isGrounded && isSprinting)
        {
            //source.Stop();
            walking = false;
        }else if (Input.GetAxis("Horizontal") == 0 && isGrounded && !walking && !isSprinting)
        {
            walkingSounds.Stop();
        }


        //jump
            if (isGrounded)
        {
            anim.SetBool("Falling", false);
            //reset jumps
            extraJumps = extrajumpsMax;
            //reset gravity
            rb.gravityScale = originalGravity;

            anim.SetBool("Gliding", false);
        }

        if (Input.GetButtonDown("Jump") && extraJumps > 0)
        {
            //emit jump particles
            myJump.Emit(6);

            //disable glide
            rb.gravityScale = originalGravity;
            anim.SetTrigger("Jump");
            //jump
            //walkingSounds.Stop();
            powerSounds.pitch = Random.Range(lowPitch, highPitch);
            powerSounds.PlayOneShot(jumpClip);
            rb.velocity = Vector2.up * jumpForce;
            //minus one jump
            extraJumps--;
            
        }
        else if (hasJump && Input.GetButtonDown("Jump") && extraJumps == 0 && isGrounded)
        {
            //emit jump particles
            myJump.Emit(6);

            //jump from ground without reducing extra jumps
            rb.velocity = Vector2.up * jumpForce;
            anim.SetTrigger("Jump");
            //walkingSounds.Stop();
            powerSounds.pitch = Random.Range(lowPitch, highPitch);
            powerSounds.PlayOneShot(jumpClip);
        }

        if (rb.velocity.y < 0 && isGliding == false)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump") && isGliding == false)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
        //dash
        if (hasDash && dashReady && Input.GetButtonDown("Dash"))
        {
            walkingSounds.Stop();
            powerSounds.pitch = Random.Range(lowPitch, highPitch);
            powerSounds.PlayOneShot(dashClip);
            //powerSounds.pitch = 1;
            walkingSounds.Play();
            StartCoroutine("Dash");
        }
        //glide
        if (hasGlide && Input.GetButtonDown("Glide") && rb.velocity.y <= 0 && !isGrounded)
        {
            anim.SetBool("Gliding", true);
            powerSounds.pitch = Random.Range(lowPitch, highPitch);
            powerSounds.PlayOneShot(glideClip);
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
            powerSounds.pitch = Random.Range(lowPitch, highPitch);
            powerSounds.PlayOneShot(glideClip);
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
        myDash.Play(withChildren: true);
        speed += dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed -= dashSpeed;
        dashReady = false;
        myDash.Stop();
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
            walkingSounds.Stop();
            walkingSounds.clip = runClip;
            walkingSounds.Play();
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
