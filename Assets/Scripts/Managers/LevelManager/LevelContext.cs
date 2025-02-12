using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContext : MonoBehaviour
{

    private GameContext _gameContext;
    public GameContext GameContext => _gameContext;

    private int _CounterPlanetsFloor;
    private int _CounterStarsFloor;

    public int CounterPlanetsFloor => _CounterPlanetsFloor;
    public int CounterStarsFloor => _CounterStarsFloor;

    public void Initialize() 
    {
        _CounterPlanetsFloor = 0;
        _CounterStarsFloor = 0;
        _gameContext = FindObjectOfType<GameContext>();
    }


    private void Update()
    {
        if (_CounterPlanetsFloor == 8) _gameContext.IncrementCounterLED();
        if (_CounterStarsFloor == 2) _gameContext.IncrementCounterLED();
    }

}
