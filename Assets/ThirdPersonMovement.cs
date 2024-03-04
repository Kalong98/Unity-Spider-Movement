using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public SpiderMovementControls spiderControls;
    public Transform groundCheck;
    public LayerMask groundMask;

    public float moveSpeed = 5.0f;
    public float turnSmoothTime = 0.1f;
    public float jumpHeight = 3.0f;
    public float gravity = -9.81f * 1.5f;
    public float groundDistance = 0.4f;
    float turnSmoothVelocity;
    Vector3 velocity;
    bool isGrounded;

    private InputAction move;
    private InputAction fire;
    private InputAction jump;

    private void Awake()
    {
        spiderControls = new SpiderMovementControls();
    }

    private void OnEnable()
    {
        move = spiderControls.Player.Move;
        move.Enable();

        fire = spiderControls.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

        jump = spiderControls.Player.Jump;
        jump.Enable();
        jump.performed += Jump;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        jump.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");
        //Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Vector2 direction = move.ReadValue<Vector2>();
        if (direction.magnitude >= 0.1f)
        {
            Move(direction);
        }
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void Move(Vector2 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.y) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 movedir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        controller.Move(movedir.normalized * moveSpeed * Time.deltaTime);
    }

    private void Fire(InputAction.CallbackContext context)
    {
        Debug.Log("fired");
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
}
