using UnityEngine;

public class SpiderStateManager : MonoBehaviour
{
    SpiderBaseState currentState;
    public SpiderIdleState idleState;
    public SpiderWalkState walkState;
    public SpiderJumpState jumpState;
    public SpiderDashState dashState;
    private SpiderPlayer player;
    private SpiderInputHandler inputHandler;

    public void Initialize(SpiderPlayer player, SpiderInputHandler inputHandler)
    {
        this.player = player;
        this.inputHandler = inputHandler;
        InitializeStates();
    }

    // Start is called before the first frame update
    private void InitializeStates()
    {
        idleState = new SpiderIdleState(player, inputHandler);
        walkState = new SpiderWalkState(player, inputHandler);
        jumpState = new SpiderJumpState(player, inputHandler);
        dashState = new SpiderDashState(player, inputHandler);
        currentState = idleState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(SpiderBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
