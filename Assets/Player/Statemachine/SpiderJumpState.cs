using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderJumpState : SpiderBaseState
{
    private SpiderPlayer player;
    private SpiderInputHandler inputHandler;
    private Vector2 direction;
    bool wasGrounded;

    public SpiderJumpState(SpiderPlayer player, SpiderInputHandler inputHandler)
    {
        this.player = player;
        this.inputHandler = inputHandler;
    }

    public override void EnterState(SpiderStateManager spider)
    {
        Debug.Log("try jumping");
        if (player.GetIsGrounded())
        {
            player.Jump();
            Material newMaterial = Resources.Load<Material>("Player/StateMaterial/Yellow");
            GameObject.Find("Spider_LP.002").GetComponent<SkinnedMeshRenderer>().material = newMaterial;
            wasGrounded = true;
        }
    }

    public override void UpdateState(SpiderStateManager spider)
    {
        if (inputHandler.GetFireTrigger())
        {
            player.Fire();
            inputHandler.ResetFireTrigger();
        }
        direction = inputHandler.GetDirection();
        if (direction.magnitude >= 0.1f)
        {
            player.Move(direction, 1);
        }
        if (player.GetIsGrounded())
        {
            if (!wasGrounded)
            {
                inputHandler.ResetAllTriggers();
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
        wasGrounded = player.GetIsGrounded();
    }
}
