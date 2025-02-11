using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections; // NECESSARIO per IEnumerator

public class LevelManager : MonoBehaviour
{
    void Awake() {
        
    }

    void OnDestroy() {
        
    }

    private void GameManager_OnGameStateChanged() {
        
    }

    //public LevelEvents levelEvents;
    //public PlayerEvents playerEvents;
    //private GameObject player;

    //public static LevelManager instance;

    //private void Awake()
    //{
    //    // Ottieni i riferimenti dal GameManager
    //    levelEvents = GameManager.instance.levelEvents;
    //    playerEvents = GameManager.instance.playerEvents;

    //    // Se non esistono, fai un controllo per essere sicuro
    //    if (levelEvents == null || playerEvents == null)
    //    {
    //        Debug.LogError("LevelEvents o PlayerEvents non sono stati assegnati nel GameManager!");
    //    }

    //    if (instance == null)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private void Start()
    //{
    //    player = GameObject.FindWithTag("Player"); // Trova il player nella scena
    //    if (player == null)
    //    {
    //        Debug.LogError("Player non trovato nella scena!");
    //        return;
    //    }

    //    if (levelEvents.HasSavedPosition())
    //    {
    //        Vector3 savedPosition = levelEvents.GetSavedPlayerPosition();
    //        player.transform.position = savedPosition;
    //        Debug.Log($"Giocatore riposizionato a: {savedPosition}");
    //    }
    //    else
    //    {
    //        Debug.Log("Nessuna posizione salvata, spawn nel punto di default.");
    //    }
    //}



    //private void OnEnable()
    //{
    //    Debug.Log("LevelManager attivato: registrazione evento OnLevelChangeRequested");
    //    levelEvents.OnLevelChangeRequested += LoadLevel;
    //}

    //private void OnDisable()
    //{
    //    levelEvents.OnLevelChangeRequested -= LoadLevel;
    //}

    //private void LoadLevel(int floorIndex)
    //{
    //    //Debug.Log($"Posizione del player prima di cambiare piano: {player.transform.position}");

    //    string sceneName = "Piano_" + floorIndex.ToString();
    //    SceneManager.LoadScene(sceneName);

    //    //StartCoroutine(SetPlayerPositionAfterSceneLoad());
    //}

    //private IEnumerator SetPlayerPositionAfterSceneLoad()
    //{
    //    yield return null; // Aspetta un frame per assicurarti che tutto sia caricato

    //    GameObject player = GameObject.FindWithTag("Player");
    //    if (player != null)
    //    {
    //        player.transform.position = playerEvents.GetSavedPlayerPosition();
    //        Debug.Log($"Giocatore riposizionato dopo il cambio scena: {player.transform.position}");
    //    }
    //    else
    //    {
    //        Debug.LogError("Player non trovato dopo il cambio scena!");
    //    }
    //}


}
