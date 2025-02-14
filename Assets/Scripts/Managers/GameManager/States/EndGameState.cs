using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameState : GameState
{
    public EndGameState(GameContext context, GameStateMachine.EGameStateMachine estate) : base(context, estate) { }

    public override void EnterState()
    {
        Debug.Log("ENTERING END GAME STATE");
        SceneManager.LoadScene("CutSceneEND");
    }
    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        vp.playbackSpeed = vp.playbackSpeed / 10.0F;
    }
    public override void UpdateState() { }

    public override void ExitState()
    {
        
    }

    public override GameStateMachine.EGameStateMachine GetNextState()
    {


        return StateKey;
    }
}
