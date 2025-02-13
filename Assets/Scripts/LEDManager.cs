using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LEDManager : MonoBehaviour
{
    public Light LED1;
    public Light LED2;
    public Light LED3;
    private GameContext _gameContext;

    void Start()
    {
        // Ottieni il GameContext
        _gameContext = FindObjectOfType<GameContext>();

        // Registra il metodo che verrà chiamato quando una scena viene caricata
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scena caricata: " + scene.name);

        if (scene.name == "Piano_0") // Controlla se Piano_0 è stato caricato
        {
            // Chiamata per aggiornare lo stato dei LED
            UpdateLEDs();
        }
    }

    void Update()
    {
        if (_gameContext.CurrentFloor == "Piano_0")
        {
            // Verifica e aggiorna i LED ogni frame in base al valore di CounterLED
            UpdateLEDs();
        }
    }

    private void UpdateLEDs()
    {
        if (LED1 == null || LED2 == null || LED3 == null) return; // Evita errori se i LED non sono ancora assegnati

        // Abilita i LED in base al valore di CounterLED
        if (_gameContext.CounterLED >= 1)
            LED1.enabled = true;
        else
            LED1.enabled = false;

        if (_gameContext.CounterLED >= 2)
            LED2.enabled = true;
        else
            LED2.enabled = false;

        if (_gameContext.CounterLED >= 3)
            LED3.enabled = true;
        else
            LED3.enabled = false;
    }

    void OnDestroy()
    {
        // Rimuovi il listener per evitare memory leak
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
