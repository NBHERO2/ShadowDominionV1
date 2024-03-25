using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    public bool isTouchingWall;
    public bool isGrounded;
    public bool grabInput;
    public bool isTouchingLedge;
    protected int xInput;
    protected int yInput;
    protected bool jumpInput;
    
    public PlayerTouchingWallState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
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
        isTouchingWall = player.CheckIfTouchingWall();
        isGrounded = player.CheckIfTouchingGround();
        isTouchingLedge = player.CheckIfTouchingLedge();

        if (isTouchingWall && !isTouchingLedge)
        {
            player.LedgeClimbState.SetDetectedPosition(player.transform.position);
        }
    }


    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;
        xInput = player.InputHandler.NormInputX;
        yInput = player.InputHandler.NormInputY;
        if (jumpInput)
        {
            player.InputHandler.UseJumpInput();
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            playerStateMachine.ChangeState(player.WallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            playerStateMachine.ChangeState(player.StateIdle);
        } else if (!isTouchingWall || ( xInput != player.FacingDirection && !grabInput))
        {
            playerStateMachine.ChangeState(player.InAirState);
        } else if(isTouchingWall && !isTouchingLedge)
        {
            playerStateMachine.ChangeState(player.LedgeClimbState);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
