using UnityEngine;
using UnityEngine.SceneManagement;  // Necessario per gestire il cambio di scena

public class GameManager : MonoBehaviour
{
    // Funzione chiamata quando si preme il pulsante "Start"
    public void StartGame()
    {
        // Cambia scena (usa il nome della scena che vuoi caricare)
        SceneManager.LoadScene("Scena2");  // Sostituisci "NomeDellaScena" con il nome della scena che vuoi caricare
    }
}
