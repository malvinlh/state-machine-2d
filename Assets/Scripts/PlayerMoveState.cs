using UnityEngine;

public class PlayerMoveState : BaseState<PlayerState>
{
    private PlayerStateMachine manager;

    public PlayerMoveState(PlayerStateMachine manager) : base(PlayerState.Move)
    {
        this.manager = manager;
    }

    public override void EnterState()
    {
        manager.Animator.SetBool("isMoving", true);
    }

    public override void ExitState() {}

    public override void UpdateState()
    {
        var input = manager.InputVector;

        manager.Animator.SetFloat("XInput", input.x);
        manager.Animator.SetFloat("YInput", input.y);

        if (input.magnitude < 0.1f)
        {
            manager.RequestTransition(PlayerState.Idle);
        }
    }

    public override void FixedUpdateState()
    {
        var input = manager.InputVector;

        if (input.magnitude > 0.1f)
        {
            Vector2 moveDelta = input.normalized * manager.moveSpeed * Time.fixedDeltaTime;
            manager.Rb2D.MovePosition(manager.Rb2D.position + moveDelta);
        }
    }
}
