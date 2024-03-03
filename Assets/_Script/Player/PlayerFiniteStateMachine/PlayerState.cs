using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected Player player;
    protected PlayerStateMachine playerStateMachine;
    protected PlayerData playerData;

    protected bool isAnimationFinished;
    protected bool isExitingState;

    protected float startTime;
    private string animBoolName;
    
    public PlayerState(Player player, PlayerStateMachine playerStateMachine, PlayerData playerData, string animBoolName)
    {
        this.player = player;
        this.playerStateMachine = playerStateMachine;
        this.playerData = playerData;
        this.animBoolName = animBoolName;
    }
    
    // trang thai bat dau khi goi mot trang thai cu the
    public virtual void Enter()
    {
        DoChecks();
        player.Anim.SetBool(animBoolName, true);
        startTime = Time.time;
        isAnimationFinished = false;
        isExitingState = false;
        Debug.Log(animBoolName);
    }

    // trang thai ket thuc khi thoat ra khoi trang thai
    public virtual void Exit()
    {
        player.Anim.SetBool(animBoolName, false);
        isExitingState = true;
    }

    // cap nhat logic game 
    public virtual void LogicUpdate()
    {

    }

    //cap nhat vat ly game duocwj goi tu logic game
    public virtual void PhysicUpdate()
    {
        DoChecks();
    }
    // check trang thai
    public virtual void DoChecks() { }

    public virtual void AnimationTrigger() { }

    public virtual void AnimationFinishTrigger() => isAnimationFinished = true;

}
