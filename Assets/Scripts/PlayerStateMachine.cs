using UnityEngine;

public class PlayerStateMachine : StateMachine<PlayerState>
{
    [HideInInspector] public Animator Animator;
    [HideInInspector] public Rigidbody2D Rb2D;
    [HideInInspector] public Vector2 InputVector;
    [HideInInspector] public float moveSpeed = 5f;

    protected override void Start()
    {
        base.Start();
        
        Animator = GetComponent<Animator>();
        Rb2D = GetComponent<Rigidbody2D>(); // Tambahkan ini!

        RegisterState(new PlayerIdleState(this));
        RegisterState(new PlayerMoveState(this));

        SetInitialState(PlayerState.Idle);
    }

    protected override void Update()
    {
        InputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        base.Update();
    }
}