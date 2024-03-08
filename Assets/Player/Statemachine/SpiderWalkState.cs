using UnityEngine;

public class SpiderWalkState : SpiderBaseState
{
    private SpiderPlayer player;
    private SpiderInputHandler inputHandler;
    Vector2 direction;

    public SpiderWalkState(SpiderPlayer player, SpiderInputHandler inputHandler)
    {
        this.player = player;
        this.inputHandler = inputHandler;
    }

    public override void EnterState(SpiderStateManager spider)
    {
        Debug.Log("Walking");
        Material newMaterial = Resources.Load<Material>("Player/StateMaterial/Green");
        GameObject.Find("Spider_LP.002").GetComponent<SkinnedMeshRenderer>().material = newMaterial;
    }

    public override void UpdateState(SpiderStateManager spider)
    {
        direction = inputHandler.GetDirection();
        if (inputHandler.GetFireTrigger())
        {
            player.Fire();
            inputHandler.ResetFireTrigger();
        }
        if (inputHandler.GetDashTrigger())
        {
            if (player.CheckDashCooldown())
            {
                spider.SwitchState(spider.dashState);
                inputHandler.ResetDashTrigger();
                return;
            }
            else
            {
                inputHandler.ResetDashTrigger();
            }
        }
        if (inputHandler.GetJumpTrigger())
        {
            spider.SwitchState(spider.jumpState);
            inputHandler.ResetJumpTrigger();
            return;
        }
        else if (direction.magnitude >= 0.1f)
        {
            player.Move(direction, 1);
        }
        else if (direction.magnitude == 0f)
        {
            spider.SwitchState(spider.idleState);
            return;
        }
    }
}
