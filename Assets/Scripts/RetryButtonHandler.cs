using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButtonHandler : MonoBehaviour
{
    public string sceneToReload; // Nome della scena da ricaricare, impostato nell'Inspector

    void Start()
    {
       //Associa la funzione al bottone
       Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {
        if (!string.IsNullOrEmpty(sceneToReload))
        {
            StartCoroutine(ReloadSceneAdditively());
        }
        else
        {
            Debug.LogError("Scene name is empty. Please set the scene name in the Inspector.");
        }
    }

    IEnumerator ReloadSceneAdditively()
    {
        Scene scene = SceneManager.GetSceneByName(sceneToReload);

        if (!scene.IsValid())
        {
            Debug.LogError("Scene not found: " + sceneToReload);
            yield break;
        }

        // Ricarica la scena in modalità Additive
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToReload, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            Debug.Log("Scena non ancora caricata.");
            yield return null;
        }
        Debug.Log("Scena caricata.");

        // Scarica la scena in modo asincrono
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(sceneToReload);

        while (!asyncUnload.isDone)
        {
            Debug.Log("Scena non ancora scaricata.");
            yield return null;
        }

        Debug.Log("Scena scaricata.");
        
    }

}
