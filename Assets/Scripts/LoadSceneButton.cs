using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Assicurati di includere questo namespace se stai usando un bottone UI
using System.Collections;

public class LoadSceneButton : MonoBehaviour
{
    public Camera secondaryCamera;
    public string sceneToUnload; // Nome della scena da scaricare, impostato nell'Inspector

    private Camera _mainCamera;

    private LevelContext _levelContext;

    private CanvasGroup _mirinoCanvasGroup;

    void Awake()
    {
        _levelContext = FindObjectOfType<LevelContext>();
    }

    void Start()
    {
        _mainCamera = Camera.main;

        GameObject _mirino = GameObject.Find("Mirino");
        if (_mirino != null)
        {
            _mirinoCanvasGroup = _mirino.GetComponent<CanvasGroup>();

            //// Se il CanvasGroup non esiste, lo aggiunge automaticamente
            //if (_mirinoCanvasGroup == null)
            //{
            //    _mirinoCanvasGroup = _mirino.AddComponent<CanvasGroup>();
            //}
        }

        //Associa la funzione al bottone
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnButtonClick);
        }
    }



    void OnButtonClick()
    {
        if (secondaryCamera == null) return; // Evita errori se la camera non è trovata

        secondaryCamera.gameObject.SetActive(false);
        // Blocca e nascondi il cursore
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Disabilita la MainCamera una volta caricata la scena
        if (_mainCamera != null)
        {
            _mainCamera.gameObject.SetActive(true);
        }
        _levelContext.IncrementCounterStarsFloor();
        _mirinoCanvasGroup.alpha = 1f;
        StartCoroutine(UnloadSceneAsync());

    }

    IEnumerator UnloadSceneAsync()
    {
        Scene scene = SceneManager.GetSceneByName(sceneToUnload);
        AsyncOperation asyncLoad = SceneManager.UnloadSceneAsync(scene.buildIndex);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        Cursor.lockState = CursorLockMode.Locked; // Blocca il cursore
        Cursor.visible = false; // Rende invisibile il cursore
    }
}