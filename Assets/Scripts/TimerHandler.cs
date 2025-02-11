using System;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    public float timerDuration = 5f; // Durata del timer in secondi
    private float timer;
    private bool isTimerRunning = false;

    // Questo evento viene usato per notificare alla GameStateMachine quando il timer è 
    private GameContext _gameContext;

    void Start()
    {
        // Inizializza il timer
        timer = timerDuration;

        // Otteniamo il GameContext nella scena
        _gameContext = FindObjectOfType<GameContext>();

        if (_gameContext == null)
            Debug.LogError("GameContext not found in the scene.");
        else
            StartTimer();
    }

    void Update()
    {
        // Se il timer è in esecuzione, aggiorna il conteggio
        if (isTimerRunning)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                // Il timer è scaduto, invoca l'evento
                StopTimer();
                _gameContext.OnTimerFinished(); // Notifica il cambio di stato
            }
        }
    }

    // Metodo per avviare il timer
    public void StartTimer()
    {
        timer = timerDuration;
        isTimerRunning = true;
    }

    // Metodo per fermare il timer
    public void StopTimer()
    {
        isTimerRunning = false;
    }
}
