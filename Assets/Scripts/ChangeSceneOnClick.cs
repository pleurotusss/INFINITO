using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneOnClick : MonoBehaviour
{
    public string sceneToLoad; // Imposta questa variabile dall'Inspector con il nome della scena da caricare

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click sinistro del mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // Se l'oggetto cliccato è quello con lo script
                {
                    SceneManager.LoadScene(sceneToLoad);
                }
            }
        }
    }
}



   
