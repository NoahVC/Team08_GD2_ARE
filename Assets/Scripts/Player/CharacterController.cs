using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    [Header("Walking")]
    [SerializeField] Transform orientation = null;
    [SerializeField] float walkSpeed = 4.0f;
    [SerializeField] float acceleration = 10.0f;
    [Header("Jumping")]
    public float jumpForce = 6f;

    [Header("Climbing")]

    public bool canClimb = false;
    public bool isClimbing = false;
    public float climbSpeed = 2f;
    public bool isInsideVine = false;

    [Header("Multiplier")]
    [SerializeField] float movementMultiplier = 6f;
    [SerializeField] float airMultiplier = 2f;

    [Header("Drag")]
    [SerializeField] float groundDrag = 6f;
    [SerializeField] float airDrag = 2f;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck = null;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] bool isGrounded = false;

    float playerHeight = 2f;

    private Rigidbody rb;
    [SerializeField] float verticalMovement;
    [SerializeField] float horizontalMovement;
    private Vector3 moveDirection;
    [SerializeField] float moveSpeed;
    private KeyCode jumpKey = KeyCode.Space;
    RaycastHit slopeHit;
    Vector3 slopeMoveDirection;
    // Start is called before the first frame update
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight))
        {
            if (slopeHit.normal != Vector3.up)
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
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void FixedUpdate()
    {
        MovePlayer();
        ControlDrag();
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
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
    }
    void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;
    }
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
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            rb.useGravity = false;
            Debug.Log("on trigger");
            rb.constraints = RigidbodyConstraints.FreezePositionZ;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        canClimb = true;
        if (other.CompareTag("Ladder") && canClimb)
        {
            isClimbing = true;
            rb.velocity = (orientation.up * verticalMovement * climbSpeed) + (orientation.right * horizontalMovement * climbSpeed);
        }
        if (other.CompareTag("Ladder") && verticalMovement < 0 && isGrounded)
        {
           
            rb.useGravity = true;
            canClimb = false;
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation;
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
            canClimb = false;

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

        if (isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Acceleration);
        }
    }
    void ControlSpeed()
    {

        moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);

    }
}


