using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{

    private bool _isStartButtonPressed;
    public bool IsStartButtonPressed => _isStartButtonPressed;

    private bool _isTimerFinished;
    public bool IsTimerFinished => _isTimerFinished;

    public void Initialize()
    {

    }

    public void OnTimerFinished()
    {
        Debug.Log("Sono nel GameContext: OnTimerFinished().");
        _isTimerFinished = true;
    }

    public void OnStartButtonPressed() {
        Debug.Log("Sono nel GameContext: OnStartButtonPressed().");
        _isStartButtonPressed = true;
    }
}
