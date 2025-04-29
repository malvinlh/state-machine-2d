using UnityEngine;
using System;

// Base class untuk setiap state dalam FSM (Finite State Machine)
// EState adalah tipe enum yang merepresentasikan nama-nama state (seperti Idle, Move, Attack)
public abstract class BaseState<EState> where EState : Enum
{
    // Properti unik untuk menyimpan tipe state (misal: PlayerState.Idle)
    public EState StateKey { get; private set; }

    // Konstruktor: wajib memberikan identitas state saat dibuat
    public BaseState(EState key)
    {
        StateKey = key;
    }

    // Method yang akan dipanggil saat state ini pertama kali aktif
    public abstract void EnterState();

    // Method yang dipanggil saat state keluar/diganti
    public abstract void ExitState();

    // Dipanggil setiap frame (Update biasa)
    public abstract void UpdateState();

    // Dipanggil setiap fixed frame (untuk physics & movement berbasis Rigidbody)
    public abstract void FixedUpdateState();
}
