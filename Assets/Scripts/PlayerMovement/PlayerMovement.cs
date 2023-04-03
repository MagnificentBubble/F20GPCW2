using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float RunSpeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;

    bool readyToJump;
    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode RunKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public float radius;

    private float distancetoObject;
    private float nearestObjectdistance;

    private Transform nearestObject;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;
    

    private Animator anim;
    private bool isRunning;

    float xRotation;
    float yRotation;

    private void Start() 
    {
        radius=4;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        anim = GetComponentInChildren<Animator>();
        nearestObjectdistance=radius;
        nearestObject=null;
       
    }

    private void Update() 
    {
        CheckForObject();
        PlayerRotation();
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight *0.5f +0.2f, whatIsGround);

        MyInput();
        SpeedControl();

        Vector3 velocity = rb.velocity;

        anim.SetFloat("isWalking", velocity.magnitude);

        if (Input.GetKey(RunKey))
        {
            isRunning = true;
        }
        else 
        {
            isRunning = false;
        }
        

        // drag

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate() 
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //jump

        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;
            jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed *10f, ForceMode.Force);
            if(Input.GetKey(RunKey))
            {
                rb.AddForce(moveDirection.normalized * RunSpeed *10f, ForceMode.Force);
            }
        }   

        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier *10f, ForceMode.Force);
        } 
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        // limiting speed
        if (flatVel.magnitude > moveSpeed && !Input.GetKey(RunKey))
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
        else if(flatVel.magnitude > RunSpeed && Input.GetKey(RunKey))
        {
            Vector3 limitedVel = flatVel.normalized * RunSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void jump()
    {
        // reset y velocity

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void PlayerRotation()
    
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime *400;   
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime *400;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        

        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
    private void CheckForObject()
    {
        nearestObjectdistance = radius;
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider c in colliders)
        {
            if(c.CompareTag("FixerHat")||c.CompareTag("Rubble")){
                distancetoObject=(transform.position-c.transform.position).sqrMagnitude;
                if(distancetoObject<nearestObjectdistance){
                    nearestObjectdistance=distancetoObject;
                    nearestObject=c.GetComponent<Transform>();
                    // Debug.Log(nearestObject);
                }
            }
        }

        if (nearestObject!=null){
            if(nearestObject.CompareTag("FixerHat")){
                Debug.Log("Hat");
                if(Input.GetKeyDown(KeyCode.F) && PlayerInventory.childExists == false)

                    {
                        nearestObject.GetComponent<HatBehaviour>().Throw();
                
                    } 

        }
            else if(nearestObject.CompareTag("Rubble")){
                if(Input.GetKeyDown(KeyCode.F))
                    {
                        nearestObject.GetComponent<ObjectThrow>().Pickup();
                    } 

        }
        }        
        
    }


}

