using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChangeSceneOnClick : MonoBehaviour
{
    public string sceneToLoad; // Imposta questa variabile dall'Inspector con il nome della scena da caricare
    private Camera mainCamera;
    private bool sceneLoaded = false;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !sceneLoaded) // Click sinistro del mouse
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
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
        if (GameObject.FindGameObjectWithTag("SecondaryCam") == null)
        {
            sceneLoaded = false;
            mainCamera.gameObject.SetActive(true);
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
        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
            sceneLoaded = true;
        }
    }
}





   
