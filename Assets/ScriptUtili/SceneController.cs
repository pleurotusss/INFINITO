using UnityEngine;
using UnityEngine.SceneManagement;  // Per caricare le scene

public class SceneController : MonoBehaviour
{
    // Nome della scena da caricare dopo 10 secondi
    public string nextSceneName = "Scena3";  // Cambia con il nome della tua terza scena

    void Start()
    {
        // Chiama la funzione che cambia scena dopo 10 secondi
        Invoke("LoadNextScene", 10f);
    }

    // Funzione che carica la terza scena
    void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
