using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;
    public PlayerWallGrabState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        holdPosition = player.transform.position;
        HoldPosition();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!isExitingState)
        {
            HoldPosition();
            if (yInput > 0f)
            {
                playerStateMachine.ChangeState(player.WallClimbState);
            }
            else if (yInput < 0f || !grabInput)
            {
                playerStateMachine.ChangeState(player.WallSlideState);
            }
        }
    }

    public void HoldPosition()
    {
        player.transform.position = holdPosition;
        player.SetVelocityY(0f);
        player.SetVelocityX(0f);
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
