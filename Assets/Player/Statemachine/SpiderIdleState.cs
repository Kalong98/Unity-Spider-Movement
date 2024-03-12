using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpiderIdleState : SpiderBaseState
{
    private SpiderPlayer player;
    private SpiderInputHandler inputHandler;
    private Vector2 direction;

    public SpiderIdleState(SpiderPlayer player, SpiderInputHandler inputHandler)
    {
        this.player = player;
        this.inputHandler = inputHandler;
    }

    public override void EnterState(SpiderStateManager spider)
    {
        Debug.Log("idling");
        Material newMaterial = Resources.Load<Material>("Player/StateMaterial/Blue");
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
            // Prevents Dash input from being stored in idle
            inputHandler.ResetDashTrigger();
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
            spider.SwitchState(spider.walkState);
            return;
        }
    }
}
