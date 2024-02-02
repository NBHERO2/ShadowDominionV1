using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    #region State Variable
    public PlayerStateMachine StateMachine {  get; private set; }
    public PlayerIdleState StateIdle { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    [SerializeField] private PlayerData PlayerData;
    #endregion

    #region Component
    public PlayerInputHandler InputHandler { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Other Variable
    private Vector2 currentVelocity;
    public int FacingDirection {  get; private set; }
    
    private Vector2 workSpace;
    #endregion

    #region Unity CallBack Function
    private void Awake()
    {
        StateMachine = new PlayerStateMachine(); //lay ra trang thai hien tai cua nguoi choi
        StateIdle = new PlayerIdleState(this, StateMachine, PlayerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, PlayerData, "move");
    }

    private void Start()
    {
        FacingDirection = 1;
        Anim = GetComponent<Animator>();
        InputHandler = GetComponent<PlayerInputHandler>();
        RB = GetComponent<Rigidbody2D>();

        StateMachine.Initilize(StateIdle);
    }

    private void Update()
    {
        currentVelocity = RB.velocity;
        StateMachine.currentState.LogicUpdate();

    }

    private void FixedUpdate()
    {
        StateMachine.currentState.PhysicUpdate();
    }

    #endregion

    #region Set Function
    public void SetVelocityx(float velocity)
    {
        workSpace.Set(velocity, currentVelocity.y);
        RB.velocity = workSpace;
        currentVelocity = workSpace;
    }
    #endregion

    #region Check Function
    public void CheckIfFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Function
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180f, 0.0f);
    }
    #endregion
}