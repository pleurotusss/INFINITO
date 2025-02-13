using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagePlayer : MonoBehaviour
{
    public GameObject player;  // Riferimento al giocatore

    void Start()
    {
        // Se la scena è quella senza il First Person Controller, disabilita il giocatore
        if (SceneManager.GetActiveScene().name == "NomeDellaScenaUI")
        {
            if (player != null)
                player.SetActive(false); // Disabilita il giocatore senza distruggerlo
        }
        else
        {
            if (player != null)
                player.SetActive(true); // Riabilita il giocatore nella scena iniziale
        }
    }
}

public class GameManager : MonoBehaviour
{
    public static bool hasCompletedMinigame = false;
}

