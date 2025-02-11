using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyState : GameState
{
    public LobbyState(GameContext context, GameStateMachine.EGameStateMachine estate) : base(context, estate) { }

    public override void EnterState() 
    {
        SceneManager.LoadScene("MasterPlanetario");
    }

    public override void UpdateState() { }

    public override void ExitState() { }

    public override GameStateMachine.EGameStateMachine GetNextState()
    {
        return StateKey;
    }
}
