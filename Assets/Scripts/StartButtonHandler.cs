using UnityEngine;
using UnityEngine.UI;

public class ButtonStartHandler : MonoBehaviour
{
    // Riferimento al bottone UI
    public Button startButton;

    // Riferimento al GameContext
    private GameContext _gameContext;

    void Start()
    {
        // Otteniamo il GameContext nella scena
        _gameContext = FindObjectOfType<GameContext>();

        if (_gameContext == null)
            Debug.LogError("GameContext not found in the scene.");


        // Imposta l'evento del bottone
        if (startButton != null)
            startButton.onClick.AddListener(OnStartButtonPressed);
        else
            Debug.LogError("Start Button is not assigned.");
    }

    // Funzione che viene chiamata quando il bottone è premuto
    private void OnStartButtonPressed()
    {
        Debug.Log("Start Button Pressed!");

        // Chiamare il listener nel GameContext
        if (_gameContext != null)
            _gameContext.OnStartButtonPressed();
    }
}
