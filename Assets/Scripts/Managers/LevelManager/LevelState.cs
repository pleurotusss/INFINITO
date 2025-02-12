using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelState : BaseState<LevelStateMachine.ELevelStateMachine>
{
    protected LevelContext Context;

    public LevelState(LevelContext context, LevelStateMachine.ELevelStateMachine stateKey) : base(stateKey)
    {
        Context = context;
    }
}
