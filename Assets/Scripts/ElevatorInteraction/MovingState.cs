using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : ElevatorState
{
    public MovingState(ElevatorContext context, ElevatorStateMachine.EElevatorStateMachine estate) : base(context, estate)
    {
        Context = context;
    }

    public override void EnterState()
    {
        Debug.Log("ENTERING MOVING STATE");
        Context.CloseDoors();
        Context.StartCameraShake(5f, 5f); // Inizia il camera shake con una durata di 1s e intensità di 0.5
    }

    public override void UpdateState()
    {
        Debug.Log("UPDATING MOVING STATE");

        // Aggiorna il timer del camera shake
        Context.UpdateShakeTimer(Time.deltaTime);
    }

    public override void ExitState()
    {
        Debug.Log("EXITING MOVING STATE");
        Context.StopCameraShake();
        Context.Unloader();
        Context.Loader();
    }

    public override ElevatorStateMachine.EElevatorStateMachine GetNextState()
    {
        // Se il camera shake è finito, passa allo stato successivo
        if (!Context.IsShaking())
        {
            return ElevatorStateMachine.EElevatorStateMachine.Arrived;
        }

        // Se il camera shake è ancora in corso, resta in Moving
        return StateKey;
    }
}


