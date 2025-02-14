using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameContext : MonoBehaviour
{
    //private PlayableDirector director;
    

    private bool _isStartButtonPressed;
    public bool IsStartButtonPressed => _isStartButtonPressed;

    private bool _isTimerFinished;
    public bool IsTimerFinished => _isTimerFinished;

    private int _counterLED;
    public int CounterLED => _counterLED;

    private string _currentFloor;
    public string CurrentFloor => _currentFloor;

    private bool _cutsceneFinished = false;
    public bool IsCutsceneFinished => _cutsceneFinished;

    private bool _isPlanetsFloorComplete = false;
    private bool _isStarsFloorComplete = false;
    private bool _isRelativityFloorComplete = false;

    //public void OnCutsceneEnd(PlayableDirector director)
    //{
    //    cutsceneFinished = true;
    //}
    public void PlanetsFloorIsComplete() 
    { 
        _isPlanetsFloorComplete = true; 
    }
    public void StarsFloorIsComplete()
    {
        _isStarsFloorComplete = true;
    }
    public void RelativityFloorComplete()
    {
        _isRelativityFloorComplete = true;
    }
    public void CountCompletedLevels()
    { 
        _counterLED = (_isPlanetsFloorComplete ? 1 : 0) + (_isStarsFloorComplete ? 1 : 0) + (_isRelativityFloorComplete ? 1 : 0);
    }

    public void Initialize()
    {
        _counterLED = 0;
    }

    public void SetCurrentFloor(string currentFloor)
    {
        _currentFloor = currentFloor;
    }

    public void OnTimerFinished()
    {
        Debug.Log("Sono nel GameContext: OnTimerFinished().");
        _isTimerFinished = true;
    }

    public void OnStartButtonPressed()
    {  
        _isStartButtonPressed = true;
    }

    public void resetStartButtonPressed()
    {
        _isStartButtonPressed = false;
        _counterLED = 0;
    }
}