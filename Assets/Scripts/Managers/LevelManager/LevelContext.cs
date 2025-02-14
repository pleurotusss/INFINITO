using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelContext : MonoBehaviour
{
    private GameContext _gameContext;
    public GameContext GameContext => _gameContext;

    private int _counterPlanetsFloor;
    private int _counterStarsFloor;
    private int _counterRelativityFloor;

    public int CounterPlanetsFloor => _counterPlanetsFloor;
    public int CounterStarsFloor => _counterStarsFloor;

    public void Initialize() 
    {
        _counterPlanetsFloor = 0;
        _counterStarsFloor = 0;
        _counterRelativityFloor = 0;
        _gameContext = FindObjectOfType<GameContext>();
    }
    
    public void IncrementCounterPlanetsFloor()
    {
        _counterPlanetsFloor++;
        if (_counterPlanetsFloor == 8) GameContext.PlanetsFloorIsComplete();
    }
    public void IncrementCounterStarsFloor()
    {
        _counterStarsFloor++;
        if (_counterStarsFloor == 2) GameContext.StarsFloorIsComplete();
    }
    public void IncrementRelativityFloor()
    {
        _counterRelativityFloor++;
        if (_counterRelativityFloor == 1) GameContext.RelativityFloorComplete();
    }
}
