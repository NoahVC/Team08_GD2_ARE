using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("Walking")]
    [SerializeField] Transform orientation = null;
    [SerializeField] float walkSpeed = 4.0f;
    [SerializeField] float acceleration = 10.0f;
    [Header("Jumping")]
    public float jumpForce = 6f;

    [Header("Climbing")]

    public bool CanClimb = false;
    public float climbSpeed = 2f;

    [Header("Multiplier")]
    [SerializeField] float movementMultiplier = 6f;
    [SerializeField] float airMultiplier = 2f;


    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck = null;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] bool isGrounded = false;


    private Rigidbody rb;
    [SerializeField] float verticalMovement;
    [SerializeField] float horizontalMovement;
    private Vector3 moveDirection;
    [SerializeField] float moveSpeed;
    private KeyCode jumpKey = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void FixedUpdate()
    {
        MovePlayer();

    }
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        MyInput();

        //Controles

        ControlSpeed();

        //JumpChecks
        if (Input.GetKeyDown(jumpKey))
        {
            if (isGrounded)
            {
                Jump();
            }
        }
    }
    void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            rb.useGravity = false;
            Debug.Log("on trigger");
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            CanClimb = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            rb.velocity = Vector2.up * verticalMovement * climbSpeed;
        }
        if (other.CompareTag("Ladder") && isGrounded)
        {

           // moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

            Debug.LogFormat("Grounded");
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            rb.useGravity = true;
            Debug.Log("out trigger");
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            CanClimb = false;
        }
    }
    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
    }
    private void MovePlayer()
    {

        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;

        if (isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Acceleration);
        }
    }
    void ControlSpeed()
    {

        moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);

    }
}
