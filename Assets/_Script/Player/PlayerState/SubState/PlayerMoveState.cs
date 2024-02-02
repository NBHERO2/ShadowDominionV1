using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
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

        player.CheckIfFlip(xInput);

        player.SetVelocityx(playerData.movementVelocity * xInput);

        if (xInput == 0f)
        {
            playerStateMachine.ChangeState(player.StateIdle);
        }
    }

    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
}
