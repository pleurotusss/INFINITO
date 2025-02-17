using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseStateManager<EState> : MonoBehaviour where EState : Enum
{
    
    protected Dictionary<EState, BaseState<EState>> States = new Dictionary<EState, BaseState<EState>>();
    protected BaseState<EState> CurrentState;

    protected bool IsTransitioningState = false;

    void Start()
    {
        CurrentState.EnterState();
    }

    void Update() {
        EState nextStateKey = CurrentState.GetNextState();

        if (!IsTransitioningState && nextStateKey.Equals(CurrentState.StateKey))
            CurrentState.UpdateState();
        else if (!IsTransitioningState)
            TransitionToState(nextStateKey);
    }

    public void TransitionToState(EState stateKey) 
    {
        IsTransitioningState = true;
        CurrentState.ExitState();
        CurrentState = States[stateKey];
        CurrentState.EnterState();
        IsTransitioningState = false;
    }
}
