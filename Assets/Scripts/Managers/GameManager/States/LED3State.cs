using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LED3State : GameState
{
    public LED3State(GameContext context, GameStateMachine.EGameStateMachine estate) : base(context, estate) { }

    public override void EnterState()
    {
        Debug.Log("ENTERING LED3 STATE");
    }

    public override void UpdateState() { }

    public override void ExitState()
    {


    }

    public override GameStateMachine.EGameStateMachine GetNextState()
    {
        if(Context.CurrentFloor == "Piano_0")
            return GameStateMachine.EGameStateMachine.EndGame;

        return StateKey;
    }
}
