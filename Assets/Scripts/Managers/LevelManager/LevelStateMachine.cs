using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelStateMachine : BaseStateManager<LevelStateMachine.ELevelStateMachine>
{
    public enum ELevelStateMachine { StartState, PlanetsLevel, StarsLevel, RelativityLevel };

    public Transform target;
    public float stopDistance = 1f;
    public float moveSpeed = 3f;
    public float rotationSpeed = 2f;

    public GameObject dialogueBox;  
    public TextMeshProUGUI dialogueText; 

    private LevelContext _levelContext;

    private void Awake()
    {
        _levelContext = gameObject.AddComponent<LevelContext>();
        _levelContext.Initialize(target, stopDistance, moveSpeed, rotationSpeed, dialogueBox, dialogueText);
        InitizalizeStates();
    }

    private void InitizalizeStates()
    {
        States.Add(ELevelStateMachine.StartState, new StartState(_levelContext, LevelStateMachine.ELevelStateMachine.StartState));
        States.Add(ELevelStateMachine.PlanetsLevel, new PlanetsFloorState(_levelContext, LevelStateMachine.ELevelStateMachine.PlanetsLevel));
        States.Add(ELevelStateMachine.StarsLevel, new StarsFloorState(_levelContext, LevelStateMachine.ELevelStateMachine.StarsLevel));
        States.Add(ELevelStateMachine.RelativityLevel, new RelativityFloorState(_levelContext, LevelStateMachine.ELevelStateMachine.StarsLevel));

        CurrentState = States[ELevelStateMachine.StartState];
    }

}
