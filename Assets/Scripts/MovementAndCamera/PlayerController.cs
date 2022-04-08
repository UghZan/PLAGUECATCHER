using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Character Controller
    //Partially adapted from FPS Microgame's PlayerCharacterController.cs

    [Header("References")]
    public GameObject playerCamera;
    public AudioSource playerAudioSource;

    [Header("Movement")]
    public float rotationMultiplier = 10f;
    public float maxMovementSpeed = 10f;
    public float sprintMultiplier = 2f;
    public float acceleration = 10f;

    [Header("Jumping")]
    public float groundCheckDistance = 0.1f;
    public float jumpForce = 2f;
    public float additionalGravityForce = 10f;

    [Header("Misc")]
    public bool interactMode = false;
    public bool isSprinting;
    public bool isOnGround;
    public bool isMoving;
    public float stamina = 12f;
    public bool canMove = true;

    PlayerInputHandler inputHandler;
    Rigidbody rigidbody;
    CapsuleCollider collider;
    public CameraMovement camMov;

    float m_cameraAngle;
    Vector3 currentVelocity;
    bool hasJumpedInThisFrame;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<CapsuleCollider>();
        camMov = GetComponentInChildren<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            GroundCheck();

            HandleMovement();

            if (Input.GetKey(KeyCode.LeftShift) && stamina > 3f)
            {
                isSprinting = true;
            }
            else
            {
                isSprinting = false;
            }
            camMov.isSprinting = isSprinting;
            if (isSprinting)
                stamina -= Time.deltaTime;
            else
                stamina += Time.deltaTime * 0.75f;
        }
        else
        {
            currentVelocity = Vector3.zero;
        }
    }

    private void GroundCheck()
    {
        isOnGround = false;
        Ray ray = new Ray(transform.TransformPoint(collider.center), Vector3.down);
        if(Physics.Raycast(ray, out RaycastHit hit, groundCheckDistance, -1, QueryTriggerInteraction.Ignore))
        {
            isOnGround = true;
        }
    }

    void HandleMovement()
    {
        //rotations
        if (!interactMode)
        {
            //body of controller rotates horizontally
            transform.Rotate(new Vector3(0, inputHandler.GetMouseHorizontal(), 0), Space.Self);

            //camera rotates vertically
            m_cameraAngle += inputHandler.GetMouseVertical();
            m_cameraAngle = Mathf.Clamp(m_cameraAngle, -90, 90);
            playerCamera.transform.localEulerAngles = new Vector3(-m_cameraAngle, 0, 0);
        }
        //actual movement

        //accelerating our movement (should work in crouch/prone => separate multiplier)
        float sprintMul = isSprinting ? sprintMultiplier : 1f;

        Vector3 rawVelocity = transform.TransformVector(inputHandler.GetMovementVector());
        camMov.tiltDirection = inputHandler.GetMovementVector();
        Vector3 nextVelocity = rawVelocity * maxMovementSpeed * sprintMul;
        currentVelocity = Vector3.Lerp(currentVelocity, nextVelocity, acceleration * Time.deltaTime);

        isMoving = currentVelocity.magnitude > 0.25f;

        /*if (isOnGround)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                hasJumpedInThisFrame = true;
                isOnGround = false;
            }
        }*/
    }

    public void FixedUpdate()
    {
        rigidbody.MovePosition(transform.position + currentVelocity * Time.fixedDeltaTime);

        if (!isOnGround)
            rigidbody.AddForce(Vector3.down*additionalGravityForce, ForceMode.Acceleration);

        if (hasJumpedInThisFrame)
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            hasJumpedInThisFrame = false;
        }
    }
}
