using UnityEngine;
using UnityEngine.InputSystem;

public class SpiderInputHandler : MonoBehaviour
{
    public SpiderMovementControls spiderControls;

    private InputAction move;
    private InputAction fire;
    private InputAction jump;
    private InputAction dash;

    bool fireTrigger;
    bool jumpTrigger;
    bool dashTrigger;

    // Awake is called when the instance is being loaded
    private void Awake()
    {
        spiderControls = new SpiderMovementControls();
    }

    // OnEnable is called when the object becomes enabled and active
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

        dash = spiderControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;
    }

    // OnDisable is called when the object becomes disabled or inactive
    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        jump.Disable();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Setter for the triggers if input has been detected
    private void Fire(InputAction.CallbackContext context)
    {
        fireTrigger = true;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        jumpTrigger = true;
    }

    private void Dash(InputAction.CallbackContext context)
    {
        dashTrigger = true;
    }

    // Reset the trigger back to false after it has been read
    public void ResetAllTriggers()
    {
        ResetFireTrigger();
        ResetJumpTrigger();
        ResetDashTrigger();
    }

    public void ResetFireTrigger()
    {
        fireTrigger = false;
    }

    public void ResetJumpTrigger()
    {
        jumpTrigger = false;
    }

    public void ResetDashTrigger()
    {
        dashTrigger = false;
    }

    // Getter methods for accessing trigger status
    public Vector2 GetDirection()
    {
        return move.ReadValue<Vector2>();
    }
    
    
    public bool GetFireTrigger()
    {
        return fireTrigger;
    }

    public bool GetJumpTrigger()
    {
        return jumpTrigger;
    }

    public bool GetDashTrigger()
    {
        return dashTrigger;
    }
}