using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState currentState {  get; private set; }

    public virtual void Initilize(PlayerState startingState) 
    {
        currentState = startingState;
        currentState.Enter();
    }

    public virtual void ChangeState (PlayerState newState) 
    {
        currentState.Exit(); 
        currentState = newState; 
        currentState.Enter(); 
    }
}
