using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    private bool JumpInput;
    private bool isGrounded;
    private bool grabInput;
    private bool isTouchingWall;

    public PlayerGroundedState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
        isGrounded = player.CheckIfTouchingGround();
    }

    public override void Enter()
    {
        base.Enter();
        player.JumpState.ResetAmountOfJump();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        xInput = player.InputHandler.NormInputX;
        JumpInput = player.InputHandler.JumpInput;

        if (JumpInput && player.JumpState.canJump())
        {
            playerStateMachine.ChangeState(player.JumpState);
        } 
        else if (isTouchingWall && grabInput)
        {
            playerStateMachine.ChangeState(player.WallGrabState);
        }
        else if (!isGrounded)
        {
            playerStateMachine.ChangeState(player.InAirState);
        }
        
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
