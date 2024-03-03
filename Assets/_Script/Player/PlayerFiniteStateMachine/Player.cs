using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour 
{
    #region State Variable
    public PlayerStateMachine StateMachine {  get; private set; }
    public PlayerIdleState StateIdle { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    public PlayerJumpState JumpState { get; private set; } 
    public PlayerInAirState InAirState { get; private set; }
    public PlayerLandState LandState { get; private set; }
    public PlayerWallClimbState WallClimbState { get; private set; }    
    public PlayerWallGrabState WallGrabState { get; private set; }
    public PlayerWallSlideState WallSlideState { get; private set; }
    public PlayerWallJumpState WallJumpState { get; private set; }

    

    [SerializeField] private PlayerData PlayerData;
    #endregion

    #region Check Transform
    [SerializeField]protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    #endregion

    #region Component
    public PlayerInputHandler InputHandler { get; private set; }
    public Animator Anim { get; private set; }
    public Rigidbody2D RB { get; private set; }
    #endregion

    #region Other Variable
    public Vector2 currentVelocity;
    public int FacingDirection {  get; private set; }
    
    private Vector2 workSpace;
    #endregion

    #region Unity CallBack Function
    private void Awake()
    {
        StateMachine = new PlayerStateMachine(); //lay ra trang thai hien tai cua nguoi choi
        StateIdle = new PlayerIdleState(this, StateMachine, PlayerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, PlayerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, PlayerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, PlayerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, PlayerData, "land");
        WallSlideState = new PlayerWallSlideState(this, StateMachine, PlayerData, "wallSlide");
        WallClimbState = new PlayerWallClimbState(this, StateMachine, PlayerData, "wallClimb");
        WallGrabState = new PlayerWallGrabState(this, StateMachine, PlayerData, "wallGrab");
        WallJumpState = new PlayerWallJumpState(this, StateMachine, PlayerData, "inAir");
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
    public void SetVelocityX(float velocity)
    {
        workSpace.Set(velocity, currentVelocity.y);
        RB.velocity = workSpace; 
    }

    public void SetVelocityY(float velocity)
    {
        workSpace.Set(currentVelocity.x, velocity);
        RB.velocity = workSpace;
        currentVelocity = workSpace;
    }

    public void SetVelocity(float velocity, Vector2 angle, int direction)
    {
        angle.Normalize();
        workSpace.Set(angle.x * velocity * direction, angle.y * velocity);
        RB.velocity = workSpace;
        currentVelocity = workSpace;
    }

    #endregion

    #region Check Function

    public bool CheckIfTouchingGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, PlayerData.groundCheckRadius, PlayerData.whatIsGround);
    }

    public bool CheckIfTouchingWall()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * FacingDirection, PlayerData.wallCheckDistance, PlayerData.whatIsGround);
    }

    public bool CheckIfTouchingWallBack()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * -FacingDirection, PlayerData.wallCheckDistance, PlayerData.whatIsGround);
    }

    public void CheckIfFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }

    #endregion

    #region Other Function

    public void AnimationTrigger() => StateMachine.currentState.AnimationTrigger();

    public void AnimationFinishTrigger() => StateMachine.currentState.AnimationFinishTrigger();
    private void Flip()
    {
        FacingDirection *= -1;
        transform.Rotate(0.0f, 180f, 0.0f);
    }
    #endregion
}