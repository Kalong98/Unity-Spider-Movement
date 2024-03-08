using UnityEngine;

public class SpiderDashState : SpiderBaseState
{
    private SpiderPlayer player;
    private SpiderInputHandler inputHandler;
    private float dashmultiplier = 2.5f;
    private float dashDuration = 1.0f;
    private float dashTimer;
    Vector2 direction;

    public SpiderDashState(SpiderPlayer player, SpiderInputHandler inputHandler)
    {
        this.player = player;
        this.inputHandler = inputHandler;
    }

    public override void EnterState(SpiderStateManager spider)
    {
        Debug.Log("Dashing");
        Material newMaterial = Resources.Load<Material>("Player/StateMaterial/Red");
        GameObject.Find("Spider_LP.002").GetComponent<SkinnedMeshRenderer>().material = newMaterial;
        dashTimer = dashDuration;
        player.ResetDashCooldown();
    }

    public override void UpdateState(SpiderStateManager spider)
    {
        dashTimer -= Time.deltaTime;

        if (dashTimer > 0)
        {
            direction = inputHandler.GetDirection();
            if (inputHandler.GetFireTrigger())
            {
                player.Fire();
                inputHandler.ResetFireTrigger();
            }
            if (inputHandler.GetJumpTrigger())
            {
                player.Jump();
                inputHandler.ResetJumpTrigger();
            }
            else if (direction.magnitude >= 0.1f)
            {
                player.Move(direction, dashmultiplier);
            }
        }
        else 
        {
            if (inputHandler.GetJumpTrigger())
            {
                spider.SwitchState(spider.jumpState);
                inputHandler.ResetJumpTrigger();
                return;
            }
            if (direction.magnitude >= 0.1f)
            {
                spider.SwitchState(spider.walkState);
                return;
            }
            else
            {
                spider.SwitchState(spider.idleState);
                return;
            }
        }
    }
}
