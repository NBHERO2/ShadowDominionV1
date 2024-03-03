using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    private float amountOfJumpLeft;
    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
        amountOfJumpLeft = playerData.amountOfJump;
    }

    public override void Enter()
    {
        base.Enter();
        player.InputHandler.UseJumpInput();
        player.SetVelocityY(playerData.jumpVelocity);
        isAbilityDone = true;
        DecreaseAmountOfJump();
    }

    public bool canJump()
    {
        if (amountOfJumpLeft > 0)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    public void ResetAmountOfJump() => amountOfJumpLeft = playerData.amountOfJump;

    public void DecreaseAmountOfJump() => amountOfJumpLeft--;
}
