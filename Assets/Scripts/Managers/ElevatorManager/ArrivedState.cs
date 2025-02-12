using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivedState : ElevatorState
{
    public ArrivedState(ElevatorContext context, ElevatorStateMachine.EElevatorStateMachine estate) : base(context, estate) {
        Context = context;
    }

    public override void EnterState()
    {
        Debug.Log("ENTERING ARRIVED STATE");
        Context.OpenDoors();
    }

    public override void UpdateState()
    {
        Debug.Log("UPDATING ARRIVED STATE");
    }

    public override void ExitState()
    {
        Debug.Log("EXITING ARRIVED STATE");
    }

    public override ElevatorStateMachine.EElevatorStateMachine GetNextState()
    {
        // Se un bottone associato a un piano viene premuto, passo allo stato "Moving"
        if (Context.IsFloorButtonPressed()) // Funzione che verifica se il bottone di apertura porte è stato premuto
        {
            Context.SetFloorButtonPressed(false);
            return ElevatorStateMachine.EElevatorStateMachine.Moving;
        }

        return StateKey;
    }
}

