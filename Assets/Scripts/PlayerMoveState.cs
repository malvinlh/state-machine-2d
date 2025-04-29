using UnityEngine;

// State untuk Player saat bergerak (Move)
public class PlayerMoveState : BaseState<PlayerState>
{
    private PlayerStateMachine manager;

    // Constructor: saat membuat state ini, kita tahu bahwa ini adalah PlayerState.Move
    public PlayerMoveState(PlayerStateMachine manager) : base(PlayerState.Move)
    {
        this.manager = manager;
    }

    // Dipanggil saat pertama kali masuk ke state Move
    public override void EnterState()
    {
        // Set animasi ke mode moving
        manager.Animator.SetBool("isMoving", true);
    }

    // Dipanggil saat keluar dari state Move (tidak perlu apa-apa di sini)
    public override void ExitState() {}

    // Dipanggil setiap frame Update
    public override void UpdateState()
    {
        var input = manager.InputVector;

        // Update animator parameter supaya arah gerak terlihat
        manager.Animator.SetFloat("XInput", input.x);
        manager.Animator.SetFloat("YInput", input.y);

        // Kalau tidak ada input lagi (besar kecil dari 0.1), minta transisi balik ke Idle
        if (input.magnitude < 0.1f)
        {
            manager.RequestTransition(PlayerState.Idle);
        }
    }

    // Dipanggil setiap frame FixedUpdate (khusus physics)
    public override void FixedUpdateState()
    {
        var input = manager.InputVector;

        if (input.magnitude > 0.1f)
        {
            // Hitung berapa jauh harus bergerak dalam satu physics frame
            Vector2 moveDelta = input.normalized * manager.moveSpeed * Time.fixedDeltaTime;

            // Pindahkan Rigidbody ke posisi baru
            manager.Rb2D.MovePosition(manager.Rb2D.position + moveDelta);
        }
    }
}
