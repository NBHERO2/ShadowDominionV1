using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine // máy trình phát trạng tháy người chơi
{
    public PlayerState currentState {  get; private set; }

    public virtual void Initilize(PlayerState startingState) // trang thai khoi tao trinh phat
    {
        currentState = startingState;
        currentState.Enter();
    }

    public virtual void ChangeState (PlayerState newState) // thay doi trang thai
    {
        currentState.Exit(); // thoat trang thai cu
        currentState = newState; // gan trang thai moi
        currentState.Enter(); // bat dau  trang thai
    }


}
