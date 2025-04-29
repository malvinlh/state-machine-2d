using UnityEngine;
using System;
using System.Collections.Generic;

public abstract class StateMachine<EState> : MonoBehaviour where EState : Enum
{
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;

    private EState queuedState;
    private bool hasQueuedState = false;

    protected virtual void Start()
    {
        if (CurrentState != null)
            CurrentState.EnterState();
    }

    protected virtual void Update()
    {
        if (hasQueuedState)
        {
            PerformQueuedTransition();
        }
        else
        {
            CurrentState?.UpdateState();
        }
    }

    protected virtual void FixedUpdate()
    {
        CurrentState?.FixedUpdateState();
    }

    protected void PerformQueuedTransition()
    {
        if (States.TryGetValue(queuedState, out var nextState))
        {
            CurrentState?.ExitState();
            CurrentState = nextState;
            CurrentState.EnterState();
        }
        else
        {
            Debug.LogError($"Queued state {queuedState} tidak ditemukan di States dictionary.");
        }

        hasQueuedState = false;
    }

    // Request bukan langsung Transition
    public void RequestTransition(EState nextState)
    {
        queuedState = nextState;
        hasQueuedState = true;
    }

    protected void RegisterState(BaseState<EState> state)
    {
        if (!States.ContainsKey(state.StateKey))
            States.Add(state.StateKey, state);
    }

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
