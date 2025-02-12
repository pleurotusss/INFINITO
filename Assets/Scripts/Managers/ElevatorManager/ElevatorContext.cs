using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class ElevatorContext : MonoBehaviour
{
    private ElevatorStateMachine _elevatorStateMachine;

    private AutomaticDoors _elevatorDoors;
    private CameraShake _cameraShake;

    private AsyncOperation _asyncLoadOperation;

    private GameContext _gameContext;

    private int _nextFloorIndex;
    private string _nextFloorName;

    private string _currentFloor;

    private bool _isFloorButtonPressed;
    private bool _isOpenButtonPressed;
    private bool isShaking = false;

    private float shakeDuration = 0f; // Durata del camera shake
    private float shakeTimer = 0f;    // Timer per tracciare la durata

    public void Initialize(AutomaticDoors elevatorDoors, CameraShake cameraShake)
    {
        _elevatorDoors = elevatorDoors;
        _cameraShake = cameraShake;
        _gameContext = FindObjectOfType<GameContext>();
    }

    public void StartCameraShake(float duration, float intensity)
    {
        isShaking = true;
        shakeDuration = duration;
        shakeTimer = 0f;
        _cameraShake.StartShake(intensity, duration); // Passa la durata e l'intensità
    }

    public void StopCameraShake()
    {
        isShaking = false;
        _cameraShake.StopShake();
    }

    public bool IsShaking()
    {
        return isShaking;
    }

    public void UpdateShakeTimer(float deltaTime)
    {
        if (isShaking)
        {
            shakeTimer += deltaTime;
            if (shakeTimer >= shakeDuration)
            {
                isShaking = false;
                shakeTimer = 0f;
                StopCameraShake(); // Ferma il camera shake quando il timer scade
            }
        }
    }

    public void OpenDoors()
    {
        Debug.Log("Apro porte.");
        _elevatorDoors.ApriPorte();

    }

    public void CloseDoors()
    {
        Debug.Log("Chiudo porte.");
        _elevatorDoors.ChiudiPorte();
    }


    public void Loader()
    {
        StartCoroutine(LoadNextFloorScene());
    }

    private IEnumerator LoadNextFloorScene()
    {
        CloseDoors();

        _nextFloorName = "Piano_" + _nextFloorIndex.ToString();
        Scene floorToLoad = SceneManager.GetSceneByName(_nextFloorName);
        if (floorToLoad.IsValid() && floorToLoad.isLoaded)
            yield break;

        _asyncLoadOperation = SceneManager.LoadSceneAsync(_nextFloorName, LoadSceneMode.Additive);
        while (!_asyncLoadOperation.isDone)
        {
            yield return null;
        }

        _asyncLoadOperation = null;
        Scene loadedFloor = SceneManager.GetSceneByName(_nextFloorName);
        SceneManager.SetActiveScene(loadedFloor);
        SetCurrentFloor(loadedFloor.name);
    }

    public void Unloader()
    {
        StartCoroutine(UnloadFloor());
    }

    private IEnumerator UnloadFloor()
    {
        CloseDoors();

        //_currentFloor = "Piano_" + _currentFloorIndex.ToString();
        Scene floorToLoad = SceneManager.GetSceneByName(_nextFloorName);
        while (_asyncLoadOperation != null && !_asyncLoadOperation.isDone)
        {
            yield return null;
        }

        SceneManager.UnloadSceneAsync(_currentFloor);
    }

    public bool HasSceneLoaded()
    {
        throw new NotImplementedException();

    }

    public bool IsPlayerExited()
    {
        ElevatorTrigger trigger = GameObject.FindObjectOfType<ElevatorTrigger>();
        return trigger != null && trigger.HasPlayerExited();
    }

    public void SetOpenButtonPressed(bool yn)
    {
        _isOpenButtonPressed = yn;
    }

    public bool IsOpenButtonPressed()
    {
        return _isOpenButtonPressed;
    }

    public void SetFloorButtonPressed(bool yn)
    {
        _isFloorButtonPressed = yn;
    }

    public bool IsFloorButtonPressed()
    {
        return _isFloorButtonPressed;
    }

    public void SetNextFloor(int floorIndex)
    {
        _nextFloorIndex = floorIndex;
        SetFloorButtonPressed(true);
    }

    public int GetNextFloor()
    {
        return _nextFloorIndex;   
    }

    public void SetCurrentFloor(string currentFloor)
    {
        _currentFloor = currentFloor;
        _gameContext.SetCurrentFloor(currentFloor);
    }

    public string GetCurrentFloor()
    {
        return _currentFloor;
    }

}
