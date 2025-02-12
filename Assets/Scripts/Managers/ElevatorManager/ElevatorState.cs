using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Questa classe astratta intermediaria permette di definire metodi e proprietà che possono essere usati da tutti gli stati figli, senza che abbia senso in altri contesti
public abstract class ElevatorState : BaseState<ElevatorStateMachine.EElevatorStateMachine>
{
    protected ElevatorContext Context;

    public ElevatorState(ElevatorContext context, ElevatorStateMachine.EElevatorStateMachine stateKey) : base(stateKey)
    {
        Context = context; 
    }
}
