using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    public bool isTouchingWall;
    public bool isGrounded;
    public bool grabInput;
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
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
