using System;
using UnityEngine;

public class IdleState : ElevatorState
{
    public IdleState(ElevatorContext context, ElevatorStateMachine.EElevatorStateMachine estate) : base(context, estate) { }

    public override void EnterState()
    {
        Debug.Log("ENTERING IDLE STATE");
        //GameManager.Instance.SetPlayerUsedElevator(false);
        Context.CloseDoors(); // In IDLE, le porte devono rimanere chiuse
    }

    public override void UpdateState()
    {
        Debug.Log("UPDATING IDLE STATE");
    }

    public override void ExitState()
    {
        Debug.Log("EXITING IDLE STATE");
    }

    public override ElevatorStateMachine.EElevatorStateMachine GetNextState()
    {
        // Se il bottone per aprire le porte viene premuto, passa allo stato "Arrived"
        if (Context.IsOpenButtonPressed()) // Funzione che verifica se il bottone di apertura porte è stato premuto
        {
            Context.SetOpenButtonPressed(false);
            return ElevatorStateMachine.EElevatorStateMachine.Arrived;
        }

        //// Se il giocatore preme il bottone per un piano, passa a Moving
        ////if (Context.IsFloorButtonPressed())
        ////{
        ////    return ElevatorStateMachine.EElevatorStateMachine.Moving;
        ////}

        //// Rimani nello stato "Idle" se non c'è alcuna interazione
        return StateKey; // Altrimenti resta in Idle
    }

}
