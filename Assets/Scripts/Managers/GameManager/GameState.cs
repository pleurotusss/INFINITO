using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameState : BaseState<GameStateMachine.EGameStateMachine>
{
    protected GameContext Context;

    public GameState(GameContext context, GameStateMachine.EGameStateMachine stateKey) : base(stateKey)
    {
        Context = context;
    }
}
