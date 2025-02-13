using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Assicurati di includere questo namespace se stai usando un bottone UI
using System.Collections;

public class LoadSceneButton : MonoBehaviour
{
    public Camera secondaryCamera;
    public string sceneToUnload; // Nome della scena da scaricare, impostato nell'Inspector

    private Camera mainCamera;

    private LevelContext _levelContext;

    void Awake()
    {
        // Implementazione Singleton per evitare duplicati
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject); // Se esiste già un'istanza, distruggi quella nuova
        //    return;
        //}

        _levelContext = FindObjectOfType<LevelContext>();
    }

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (secondaryCamera == null) return; // Evita errori se la camera non è trovata

        if (Input.GetMouseButtonDown(0)) // Click sinistro del mouse
        {
            secondaryCamera.gameObject.SetActive(false);
            // Blocca e nascondi il cursore
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // Disabilita la MainCamera una volta caricata la scena
            if (mainCamera != null)
            {
                mainCamera.gameObject.SetActive(true);
            }
            _levelContext.IncrementCounterStarsFloor();
            StartCoroutine(UnloadSceneAsync());
        }
    }

    IEnumerator UnloadSceneAsync()
    {
        Scene scene = SceneManager.GetSceneByName(sceneToUnload);
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(scene.buildIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}