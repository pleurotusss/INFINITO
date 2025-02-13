using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ElevatorStateMachine : BaseStateManager<ElevatorStateMachine.EElevatorStateMachine>
{
    public enum EElevatorStateMachine { Idle, Moving, Arrived }
    private ElevatorContext _elevatorContext;
    
    [SerializeField] private AutomaticDoors elevatorDoors;
    [SerializeField] private CameraShake cameraShake;


    void Awake()
    {
        ValidateCostrains();

        _elevatorContext = gameObject.AddComponent<ElevatorContext>(); 
        _elevatorContext.Initialize(elevatorDoors, cameraShake);

        InitializeStates();
    }
    
    private void ValidateCostrains()
    {
        Assert.IsNotNull(elevatorDoors, "Porte non assegnate.");
        Assert.IsNotNull(cameraShake, "Camera non assegnata.");
    }

    private void InitializeStates()
    {
        States.Add(EElevatorStateMachine.Idle, new IdleState(_elevatorContext, EElevatorStateMachine.Idle));
        States.Add(EElevatorStateMachine.Moving, new MovingState(_elevatorContext, EElevatorStateMachine.Moving));
        States.Add(EElevatorStateMachine.Arrived, new ArrivedState(_elevatorContext, EElevatorStateMachine.Arrived));

        _elevatorContext.SetCurrentFloor("Piano_0");
        _elevatorContext.Loader();
        CurrentState = States[EElevatorStateMachine.Idle];

    }

    internal ElevatorContext GetContext()
    {
        return _elevatorContext;
    }
}
