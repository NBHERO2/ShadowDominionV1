using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerTouchingWallState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName) : base(player, playerStateMachine, playerData, animBoolName)
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
       

        player.SetVelocityY(-playerData.slideVelocity);
        if (!isExitingState)
        {
            if (grabInput && yInput == 0)
            {
                playerStateMachine.ChangeState(player.WallGrabState);
            }
        }
        
    }
}
