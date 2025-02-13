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

    public override void ExitState() 
    {
        Debug.Log("EXITING LOBBY STATE");
        // Carica CutScene: accensione LED 1
    }

    public override GameStateMachine.EGameStateMachine GetNextState()
    {
        // Conto quanti livelli sono completi
        Context.CountCompletedLevels();
        if (Context.CounterLED == 1)
        {
            return GameStateMachine.EGameStateMachine.LED1;
        }

        return StateKey;
    }
}
