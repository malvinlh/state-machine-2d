using UnityEngine;
using System;
using System.Collections.Generic;

// Base class FSM generik yang bisa dipakai di banyak context (misal: player, enemy, AI)
// EState adalah tipe enum yang mendefinisikan state-state-nya
public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
{
    // Menyimpan semua state yang terdaftar dalam bentuk dictionary
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();

    // State yang sedang aktif sekarang
    protected BaseState<EState> CurrentState;

    // Untuk menangani transisi tertunda ke state baru
    private EState queuedState;
    private bool hasQueuedState = false;

    // Unity Start: jika sudah ada state awal, langsung masuk ke EnterState
    protected virtual void Start()
    {
        if (CurrentState != null)
            CurrentState.EnterState();
    }

    // Dipanggil setiap frame
    protected virtual void Update()
    {
        // Kalau ada transisi yang dijadwalkan, jalankan dulu transisinya
        if (hasQueuedState)
        {
            PerformQueuedTransition();
        }
        else
        {
            // Kalau tidak, lanjutkan UpdateState() dari state aktif
            CurrentState?.UpdateState();
        }
    }

    // Dipanggil setiap fixed frame (untuk movement/physics)
    protected virtual void FixedUpdate()
    {
        CurrentState?.FixedUpdateState();
    }

    // Eksekusi perpindahan state
    protected void PerformQueuedTransition()
    {
        // Cari state berikutnya dari dictionary berdasarkan key
        if (States.TryGetValue(queuedState, out var nextState))
        {
            // Keluar dari state saat ini
            CurrentState?.ExitState();
            // Ganti state
            CurrentState = nextState;
            // Masuk ke state baru
            CurrentState.EnterState();
        }
        else
        {
            // Kalau key state tidak ditemukan, cetak error
            Debug.LogError($"Queued state {queuedState} tidak ditemukan di States dictionary.");
        }

        hasQueuedState = false; // Reset antrian transisi
    }

    // Meminta transisi ke state lain (ditunda ke Update berikutnya)
    public void RequestTransition(EState nextState)
    {
        queuedState = nextState;
        hasQueuedState = true;
    }

    // Menambahkan state ke dictionary jika belum ada
    protected void RegisterState(BaseState<EState> state)
    {
        if (!States.ContainsKey(state.StateKey))
            States.Add(state.StateKey, state);
    }

    // Menentukan state awal FSM sebelum Start() dijalankan
    protected void SetInitialState(EState stateKey)
    {
        if (States.TryGetValue(stateKey, out var initialState))
        {
            CurrentState = initialState;
        }
        else
        {
            Debug.LogError($"Initial state {stateKey} tidak ditemukan di States dictionary.");
        }
    }
}
