using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetsState : LevelState
{
    public PlanetsState(LevelContext context, LevelStateMachine.ELevelStateMachine estate) : base(context, estate)
    {
        Context = context;
    }

    private LevelStateMachine.ELevelStateMachine _nextState;

    public override void EnterState() { }

    public override void UpdateState() { }

    public override void ExitState() { }

    public override LevelStateMachine.ELevelStateMachine GetNextState()
    {
        switch (Context.GameContext.CurrentFloor)
        {
            case "Piano_-1":
                Debug.Log("LEVEL MANAGER --- STANZA PIANETI");
                _nextState = LevelStateMachine.ELevelStateMachine.PlanetsLevel;
                break;
            case "Piano-2":
                Debug.Log("LEVEL MANAGER --- STANZA STELLE");
                _nextState = LevelStateMachine.ELevelStateMachine.StarsLevel;
                break;
            case "Piano_-3":
                Debug.Log("LEVEL MANAGER --- STANZA RELATIVITÀ");
                //Setto il current state
                break;
            default:
                Debug.Log("LEVEL MANAGER --- NESSUN LIVELLO ATTIVO");
                _nextState = LevelStateMachine.ELevelStateMachine.StartState;
                break;
        }

        return _nextState;
    }
}
