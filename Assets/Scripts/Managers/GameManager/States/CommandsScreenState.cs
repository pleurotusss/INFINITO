using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CommandsScreenState : GameState
{
    public CommandsScreenState(GameContext context, GameStateMachine.EGameStateMachine estate) : base(context, estate) { }

    public override void EnterState() 
    {
        SceneManager.LoadScene("CommandsScreen");
    }

    public override void UpdateState() { }

    public override void ExitState() { }

    public override GameStateMachine.EGameStateMachine GetNextState()
    {
        if (Context.IsTimerFinished)
            return GameStateMachine.EGameStateMachine.Lobby;

        return StateKey;
    }
}
