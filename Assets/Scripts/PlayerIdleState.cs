using UnityEngine;

public class PlayerIdleState : BaseState<PlayerState>
{
    private PlayerStateMachine manager;

    public PlayerIdleState(PlayerStateMachine manager) : base(PlayerState.Idle)
    {
        this.manager = manager;
    }

    public override void EnterState()
    {
        manager.Animator.SetBool("isMoving", false);
        manager.Animator.SetFloat("XInput", 0f);
        manager.Animator.SetFloat("YInput", 0f);
    }

    public override void ExitState() {}

    public override void UpdateState()
    {
        var input = manager.InputVector;

        manager.Animator.SetFloat("XInput", input.x);
        manager.Animator.SetFloat("YInput", input.y);

        if (input.magnitude > 0.1f)
        {
            manager.RequestTransition(PlayerState.Move);
        }
    }

    public override void FixedUpdateState()
    {
        // Idle tidak perlu movement
    }
}
