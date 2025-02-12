using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateMachine : BaseStateManager<LevelStateMachine.ELevelStateMachine>
{
    public enum ELevelStateMachine { PlanetsLevel, StarsLevel };
    private LevelContext _levelContext;

    private void Awake()
    {
        _levelContext = gameObject.AddComponent<LevelContext>();
        InitizalizeStates();
    }

    private void InitizalizeStates()
    {
        
    }
}
