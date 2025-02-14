using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LED2State : GameState
{
    public LED2State(GameContext context, GameStateMachine.EGameStateMachine estate) : base(context, estate) { }

    public override void EnterState()
    {
        Debug.Log("ENTERING LED2 STATE");
    }

    public override void UpdateState() { }

    public override void ExitState()
    {


    }

    public override GameStateMachine.EGameStateMachine GetNextState()
    {
        Context.CountCompletedLevels();
        if (Context.CounterLED == 3)
            return GameStateMachine.EGameStateMachine.LED3;

        return StateKey;
    }
}
