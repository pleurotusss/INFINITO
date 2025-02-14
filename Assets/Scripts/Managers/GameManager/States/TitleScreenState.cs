using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenState : GameState
{
    public TitleScreenState(GameContext context, GameStateMachine.EGameStateMachine estate) : base(context, estate) { }

    public override void EnterState() {

        Debug.Log("ENTERING TITLESCREEN STATE");
    }

    public override void UpdateState() { }

    public override void ExitState() { }


    public override GameStateMachine.EGameStateMachine GetNextState() 
    {
        if (Context.IsStartButtonPressed)
        {
            
            return GameStateMachine.EGameStateMachine.CommandsScreen;
        }
            

        return StateKey;
    }
}
