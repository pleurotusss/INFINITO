using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Assicurati di includere questo namespace se stai usando un bottone UI

public class LoadSceneButton : MonoBehaviour
{
    public string sceneToLoad; // Nome della scena da caricare, impostato nell'Inspector

    void Start()
    {
        // Associa la funzione al bottone
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnButtonClick);
        }
    }

    void OnButtonClick()
    {
        // Carica la scena
        if (!string.IsNullOrEmpty(sceneToLoad))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
        else
        {
            Debug.LogError("Scene name is empty. Please set the scene name in the Inspector.");
        }
    }
}
