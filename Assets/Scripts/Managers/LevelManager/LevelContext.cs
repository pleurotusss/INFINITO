using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelContext : MonoBehaviour
{
    private GameContext _gameContext;
    public GameContext GameContext => _gameContext;


    private bool _mustFollowPlayer;
    public bool MustFollowPlayer => _mustFollowPlayer;


    private Transform _target;
    public Transform Target => _target;


    private float _stopDistance;
    public float StopDistance => _stopDistance;


    private float _moveSpeed;
    public float MoveSpeed => _moveSpeed;

    private float _rotationSpeed;
    public float RotationSpeed => _rotationSpeed;   


    private Transform _stopPoint;
    public Transform StopPoint => _stopPoint;

    public void SetStopPoint(Transform stopPoint)
    {
        _stopPoint = stopPoint;
    }
    public void StartRoutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }

    private GameObject _dialogueBox;
    private TextMeshProUGUI _dialogueText;

    public GameObject DialogueBox => _dialogueBox;
    public TextMeshProUGUI DialogueText => _dialogueText;

    private int _counterPlanetsFloor;
    private int _counterStarsFloor;
    private int _counterRelativityFloor;

    public int CounterPlanetsFloor => _counterPlanetsFloor;
    public int CounterStarsFloor => _counterStarsFloor;

    public void Initialize(Transform target, float stopDistance, float moveSpeed, float rotationSpeed, GameObject dialogueBox, TextMeshProUGUI dialogueText) 
    {
        _counterPlanetsFloor = 0;
        _counterStarsFloor = 0;
        _counterRelativityFloor = 0;

        _mustFollowPlayer = false;
        _target = target;
        _stopDistance = stopDistance;
        _moveSpeed = moveSpeed;
        _rotationSpeed = rotationSpeed;

        _dialogueBox = dialogueBox;
        _dialogueText = dialogueText;

        _gameContext = FindObjectOfType<GameContext>();
    }
    
    public void SetMustFollowPlayer(bool mustFollowPlayer)
    {
        _mustFollowPlayer = mustFollowPlayer;
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
