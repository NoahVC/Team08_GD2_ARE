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

    [Header("Multiplier")]
    [SerializeField] float movementMultiplier = 6f;
    [SerializeField] float airMultiplier = 2f;


    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck = null;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] bool isGrounded = false;


    private Rigidbody rb;
    private float verticalMovement;
    private float horizontalMovement;
    private Vector3 moveDirection;
    private float moveSpeed = 6f;
    private KeyCode jumpKey = KeyCode.Space;

    RaycastHit wallHit;
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
                Jump();
        }
    }
    void Jump()
    {
        rb.velocity = Vector3.up * jumpForce;
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
            rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * airMultiplier, ForceMode.Acceleration);
    }
    void ControlSpeed()
    {

        moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration * Time.deltaTime);

    }
}
