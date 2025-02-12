using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class GameStateMachine : BaseStateManager<GameStateMachine.EGameStateMachine>
{
    public enum EGameStateMachine { TitleScreen, CommandsScreen, Lobby }
    private GameContext _context;
    public static GameStateMachine Instance;
    
    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            EnsureManagersExist(); // Assicuriamoci che gli altri manager vengano istanziati
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

        CurrentState = States[EGameStateMachine.TitleScreen];
    }

    private void EnsureManagersExist()
    {
        //// Verifica se il LevelManager esiste già, altrimenti lo crea
        //if (FindObjectOfType<LevelManager>() == null)
        //{
        //    GameObject levelManagerObj = new GameObject("LevelManager");
        //    levelManagerObj.AddComponent<LevelManager>();
        //}

        //// Verifica se il PlayerManager esiste già, altrimenti lo crea
        //if (FindObjectOfType<PlayerManager>() == null)
        //{
        //    GameObject playerManagerObj = new GameObject("PlayerManager");
        //    playerManagerObj.AddComponent<PlayerManager>();
        //}
    }

  
}

