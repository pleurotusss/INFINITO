using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateMachine : BaseStateManager<LevelStateMachine.ELevelStateMachine>
{
    public enum ELevelStateMachine { StartState, PlanetsLevel, StarsLevel };
    private LevelContext _levelContext;
    //public static LevelStateMachine Instance;

    private void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

        _levelContext = gameObject.AddComponent<LevelContext>();
        _levelContext.Initialize();
        InitizalizeStates();
    }

    private void InitizalizeStates()
    {
        States.Add(ELevelStateMachine.StartState, new StartState(_levelContext, LevelStateMachine.ELevelStateMachine.StartState));
        States.Add(ELevelStateMachine.PlanetsLevel, new PlanetsState(_levelContext, LevelStateMachine.ELevelStateMachine.PlanetsLevel));
        States.Add(ELevelStateMachine.StarsLevel, new StarsState(_levelContext, LevelStateMachine.ELevelStateMachine.StarsLevel));

        CurrentState = States[ELevelStateMachine.StartState];
    }

}
