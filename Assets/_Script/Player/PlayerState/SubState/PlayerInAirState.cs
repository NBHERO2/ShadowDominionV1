using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int xInput;

    private bool isTouchingWall;

    private bool isTouchingWallBack;

    private bool isTouchingLedge;

    private bool isGrounded;

    private bool jumpInput;

    private bool grabInput;
    public PlayerInAirState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfTouchingGround();
        isTouchingWall = player.CheckIfTouchingWall();
        isTouchingWallBack = player.CheckIfTouchingWallBack();
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

        xInput = player.InputHandler.NormInputX;
        jumpInput = player.InputHandler.JumpInput;
        grabInput = player.InputHandler.GrabInput;

        if (isGrounded && player.currentVelocity.y < 0.01f)
        {
            playerStateMachine.ChangeState(player.LandState);
        }
        else if (isTouchingWall && !isTouchingLedge)
        {
            playerStateMachine.ChangeState(player.LedgeClimbState);
        }
        else if (jumpInput && (isTouchingWall || isTouchingWallBack))
        {
            player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            playerStateMachine.ChangeState(player.WallJumpState);
        }
        else if (jumpInput && player.JumpState.canJump())
        {
            playerStateMachine.ChangeState(player.JumpState);
        }
        
        else if (isTouchingWall && grabInput)
        {
            player.StateMachine.ChangeState(player.WallGrabState);
        }
        else if (isTouchingWall && xInput == player.FacingDirection && player.currentVelocity.y < 0.01f)
        {
            player.StateMachine.ChangeState(player.WallSlideState);
        } 
        else
        {
            player.CheckIfFlip(xInput);
            player.SetVelocityX(playerData.movementVelocity * xInput);
            player.Anim.SetFloat("yVelocity", player.currentVelocity.y);
            player.Anim.SetFloat("xVelocity", Mathf.Abs(player.currentVelocity.x));
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
