using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LED1State : GameState
{
    public LED1State(GameContext context, GameStateMachine.EGameStateMachine estate) : base(context, estate) { }

    public override void EnterState()
    {
       Debug.Log("ENTERING LED1 STATE");
    }

    public override void UpdateState() { }

    public override void ExitState()
    {
        

    }

    public override GameStateMachine.EGameStateMachine GetNextState()
    {
        Context.CountCompletedLevels();
        if (Context.CounterLED == 2)
            return GameStateMachine.EGameStateMachine.LED2;

        return StateKey;
    }
}
