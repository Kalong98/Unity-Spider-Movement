using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiderPlayer : MonoBehaviour
{
    private SpiderInputHandler inputHandler;
    private SpiderStateManager stateManager;
    public CharacterController controller;
    public Transform cam;
    public SpiderMovementControls spiderControls;
    public Transform groundCheck;
    public LayerMask groundMask;
    
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float jumpHeight = 3.0f;
    [SerializeField] float gravity = -9.81f * 1.5f;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] float dashCooldown = 4.0f;
    private float dashCooldownTimer = 0.0f;

    float turnSmoothVelocity;
    Vector3 velocity;
    bool isGrounded;

    void Start()
    {
        inputHandler = GetComponent<SpiderInputHandler>();
        stateManager = GetComponent<SpiderStateManager>();
        stateManager.Initialize(this, inputHandler);
        dashCooldownTimer = dashCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        if (dashCooldownTimer < dashCooldown)
        {
            dashCooldownTimer += Time.deltaTime;
        }

        // Gravity stuff
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Move(Vector2 direction, float speedMultiplier)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 movedir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(movedir.normalized * moveSpeed * speedMultiplier * Time.deltaTime);
    }

    public void Fire()
    {
        Debug.Log("fired");
        inputHandler.ResetFireTrigger();
    }

    public void Jump()
    {
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        inputHandler.ResetJumpTrigger();
    }

    public bool GetIsGrounded()
    {
        return isGrounded;
    }

    public bool CheckDashCooldown()
    {
        return dashCooldownTimer >= dashCooldown;
    }

    public void ResetDashCooldown()
    {
        dashCooldownTimer = 0f;
    }
}
