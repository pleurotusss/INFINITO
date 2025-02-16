using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeSceneOnClick : MonoBehaviour
{
    public string sceneToLoad; // Imposta questa variabile dall'Inspector con il nome della scena da caricare
    private Camera _mainCamera;
    private bool _sceneLoaded = false;
    private CanvasGroup _mirinoCanvasGroup;
    GameObject _mirino;

    private void Start()
    {
        _mainCamera = Camera.main;

        _mirino = GameObject.Find("Mirino");
        if (_mirino != null)
        {
            _mirinoCanvasGroup = _mirino.GetComponent<CanvasGroup>();

            // Se il CanvasGroup non esiste, lo aggiunge automaticamente
            if (_mirinoCanvasGroup == null)
            {
                _mirinoCanvasGroup = _mirino.AddComponent<CanvasGroup>();
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_sceneLoaded) // Click sinistro del mouse
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // Se l'oggetto cliccato è quello con lo script
                {
                    StartCoroutine(LoadScene());
                }
            }
        }

        // Controlla se esiste una camera con il tag "Secondary Cam"
        //if (GameObject.FindGameObjectWithTag("SecondaryCam") == null)
        //{
        //    _sceneLoaded = false;
        //    _mainCamera.gameObject.SetActive(true);
        //}
        if (GameObject.Find("SecondaryCamera") == null)
        {
            _sceneLoaded = false;
            _mainCamera.gameObject.SetActive(true);
        }

    }


    IEnumerator LoadScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Disabilita la MainCamera una volta caricata la scena
        if (_mainCamera != null)
        {
            _mainCamera.gameObject.SetActive(false);
            _sceneLoaded = true;
        }

        _mirinoCanvasGroup.alpha = 0f;
        Cursor.lockState = CursorLockMode.None; // Sblocca il cursore
        Cursor.visible = true; // Rende visibile il cursore

    }
}





   
