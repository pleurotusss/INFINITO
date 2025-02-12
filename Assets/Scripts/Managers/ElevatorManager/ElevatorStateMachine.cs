using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class ElevatorStateMachine : BaseStateManager<ElevatorStateMachine.EElevatorStateMachine>
{
    public enum EElevatorStateMachine { Idle, Moving, Arrived }
    private ElevatorContext _context;
    
    [SerializeField] private AutomaticDoors elevatorDoors;
    [SerializeField] private CameraShake cameraShake;


    void Awake()
    {
        ValidateCostrains();

        _context = gameObject.AddComponent<ElevatorContext>(); 
        _context.Initialize(elevatorDoors, cameraShake);

        InitializeStates();
    }

    
    private void ValidateCostrains()
    {
        Assert.IsNotNull(elevatorDoors, "Porte non assegnate.");
        Assert.IsNotNull(cameraShake, "Camera non assegnata.");
    }

    private void InitializeStates()
    {
        States.Add(EElevatorStateMachine.Idle, new IdleState(_context, EElevatorStateMachine.Idle));
        States.Add(EElevatorStateMachine.Moving, new MovingState(_context, EElevatorStateMachine.Moving));
        States.Add(EElevatorStateMachine.Arrived, new ArrivedState(_context, EElevatorStateMachine.Arrived));

        _context.SetCurrentFloor("Piano_0");
        _context.Loader();
        CurrentState = States[EElevatorStateMachine.Idle];

        //if (!GameManager.Instance.PlayerUsedElevator)
        //    CurrentState = States[EElevatorStateMachine.Idle];
        //else
        //    CurrentState = States[EElevatorStateMachine.Arrived];
    }

    internal ElevatorContext GetContext()
    {
        return _context;
    }
}
