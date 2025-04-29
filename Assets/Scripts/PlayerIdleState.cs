using UnityEngine;

// State untuk Player saat diam (Idle)
public class PlayerIdleState : BaseState<PlayerState>
{
    private PlayerStateMachine manager;

    // Constructor: saat membuat state ini, kita tahu bahwa ini adalah PlayerState.Idle
    public PlayerIdleState(PlayerStateMachine manager) : base(PlayerState.Idle)
    {
        this.manager = manager;
    }

    // Dipanggil saat pertama kali masuk ke state Idle
    public override void EnterState()
    {
        // Set animasi ke mode idle
        manager.Animator.SetBool("isMoving", false);
        manager.Animator.SetFloat("XInput", 0f);
        manager.Animator.SetFloat("YInput", 0f);
    }

    // Dipanggil saat keluar dari state Idle (tidak perlu apa-apa di sini)
    public override void ExitState() {}

    // Dipanggil setiap frame Update
    public override void UpdateState()
    {
        var input = manager.InputVector;

        // Update animator parameter (meskipun idle, input bisa sedikit berubah)
        manager.Animator.SetFloat("XInput", input.x);
        manager.Animator.SetFloat("YInput", input.y);

        // Kalau ada input arah (besar dari 0.1), minta transisi ke Move
        if (input.magnitude > 0.1f)
        {
            manager.RequestTransition(PlayerState.Move);
        }
    }

    // Dipanggil setiap frame FixedUpdate (physics)
    public override void FixedUpdateState()
    {
        // Saat Idle, tidak perlu gerakan Rigidbody
    }
}
