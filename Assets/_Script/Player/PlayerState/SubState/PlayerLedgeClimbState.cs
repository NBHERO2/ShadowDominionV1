using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    protected Vector2 detectedPos;
    protected Vector2 cornerPos;
    protected Vector2 startPos;
    protected Vector2 stopPos;

    protected int xInput;
    protected int yInput;

    protected bool isHanding;
    protected bool isClimbing;

    public PlayerLedgeClimbState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        player.Anim.SetBool("LedgeClimb", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        isHanding = true;
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        player.SetVelocityZero();
        player.transform.position = detectedPos;
        cornerPos = player.DetermineCornerPosition();

        startPos.Set(cornerPos.x - (player.FacingDirection * playerData.startOffSet.x), cornerPos.y + playerData.startOffSet.y);

        stopPos.Set(cornerPos.x + (player.FacingDirection * playerData.stopOffSet.x), cornerPos.y + playerData.stopOffSet.y);

        player.transform.position = startPos;
    }

    public override void Exit()
    {
        base.Exit();
        if(isClimbing)
        {
            player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;

        if(isAnimationFinished)
        {
            playerStateMachine.ChangeState(player.StateIdle);
        }
        else
        {
            player.SetVelocityZero();
            player.transform.position = startPos;
            if (xInput == player.FacingDirection && isHanding && !isClimbing)
            {
                isClimbing = true;
                player.Anim.SetBool("LedgeClimb", true);
            }
            else if (yInput == -1 && isHanding && !isClimbing)
            {
                playerStateMachine.ChangeState(player.InAirState);
            }
        }

    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }

    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;
}
