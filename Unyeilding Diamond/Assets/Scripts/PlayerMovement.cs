using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2d  controller;
    private Rigidbody2D rb;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;

    public int jumpCount = 0;
    public int maxJumps;
    public bool jump = false;
    public bool crouch = false;
    float horizontalMove = 0f;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        controller = GetComponent<CharacterController2d>();

    }

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal") * playerSpeed;

        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            jump = true;
            //jumpCount++;
        }

        if (Input.GetButtonDown("Crouch"))
        {
            crouch = true;
        }else if (Input.GetButtonUp("Crouch"))
        {
            crouch = false;
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false;
        
        /*groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpCount = 0;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0,0);
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            jumpCount++;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);*/
    }
}
