using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContext : MonoBehaviour
{

    private bool _isStartButtonPressed;
    public bool IsStartButtonPressed => _isStartButtonPressed;

    private bool _isTimerFinished;
    public bool IsTimerFinished => _isTimerFinished;

    private int _CounterLED;
    public int CounterLED => _CounterLED;

    private string _currentFloor;
    public string CurrentFloor => _currentFloor;
    public void SetCurrentFloor(string currentFloor)
    {
        _currentFloor = currentFloor;
    }

    public void Initialize() 
    { 
        _CounterLED = 0;
    }

    public void IncrementCounterLED()
    {
        _CounterLED += 1;
    }

    public void OnTimerFinished()
    {
        Debug.Log("Sono nel GameContext: OnTimerFinished().");
        _isTimerFinished = true;
    }

    public void OnStartButtonPressed() 
    {
        Debug.Log("Sono nel GameContext: OnStartButtonPressed().");
        _isStartButtonPressed = true;
    }
}