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
    private int _CounterPlanetsFloor;
    private int _CounterStarsFloor;

    public int CounterLED => _CounterLED;
    public int CounterPlanetsFloor => _CounterPlanetsFloor;
    public int CounterStarsFloor => _CounterStarsFloor;

    public void Initialize() 
    { 
        _CounterLED = 0;
        _CounterPlanetsFloor = 0;
        _CounterStarsFloor = 0;    
    }

    private void Update() 
    {
        if (_CounterPlanetsFloor == 8) _CounterLED++;
        if (_CounterStarsFloor == 2) _CounterLED++;
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