using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class GameStateMachine : BaseStateManager<GameStateMachine.EGameStateMachine>
{
    public enum EGameStateMachine { TitleScreen, CommandsScreen, Lobby, LED1, LED2 }
    private GameContext _context;
    public static GameStateMachine Instance;
    
    
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }

        _context = gameObject.AddComponent<GameContext>();
        InitializeStates();
    }

    private void InitializeStates()
    {
        States.Add(EGameStateMachine.TitleScreen, new TitleScreenState(_context, EGameStateMachine.TitleScreen));
        States.Add(EGameStateMachine.CommandsScreen, new CommandsScreenState(_context, EGameStateMachine.CommandsScreen));
        States.Add(EGameStateMachine.Lobby, new LobbyState(_context, EGameStateMachine.Lobby));
        States.Add(EGameStateMachine.LED1, new LED1State(_context, EGameStateMachine.LED1));
        States.Add(EGameStateMachine.LED2, new LED2State(_context, EGameStateMachine.LED2));

        CurrentState = States[EGameStateMachine.TitleScreen];
    }
  
}

