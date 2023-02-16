using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Script that controls the physics of the players movement.

    [SerializeField] float movementSpeed = 6f;
    [SerializeField] float movementMultiplier = 10f;
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;
    [SerializeField] float airMultiplier = 0.4f;
    [SerializeField] float walkSpeed = 4f;
    [SerializeField] float sprintSpeed = 6f;
    [SerializeField] float acceleration = 10f;
    
    //Added footsteps to the player character
    AudioSource audioSource;
    
    float horizontalMove;
    float verticalMove;

    bool isGrounded;
    float height = 2.0f;
    [SerializeField] float jumpForce = 5f;

    Vector3 moveDirection;
    Vector3 slopeMoveDirection;

    Rigidbody rb;

    RaycastHit slopeHit;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Checks if the user is grounded for jumping
    // Controls player drag along with thier movement co-efficients.
    // Controls movement up and down slopes also.
    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, height / 2 + 0.1f);

        MyInput();
        ControlDrag();
        CapsuleSpeed();
        PlaySound();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }

        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }

    // Checks if the user is sprinting and applies relevant values.
    void CapsuleSpeed()
    {
        if(Input.GetKey(KeyCode.LeftShift) && isGrounded)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, sprintSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, acceleration * Time.deltaTime);
        }
    }

    // Detects if the user is walking up a sloped object.
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, height / 2 + 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    // Gets the input of the user as well as their move direction.
    void MyInput()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMove + transform.right * horizontalMove;
    }

    // Controls the different drag co-efficients of the user
    void ControlDrag()
    {
        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = airDrag;
        }
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Moves the users character with physics based movement which is handled through the fixedupdate() function.
    void MovePlayer()
    {
        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if(isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * movementSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * movementSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
    }

    //Footsteps are played only when the players velocity is > 0 and also when the player is grounded.
    void PlaySound()
    {
        if (rb.velocity != Vector3.zero && isGrounded){
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
            return;
        }
        else
        {
            audioSource.Pause();
        }
    }
}
