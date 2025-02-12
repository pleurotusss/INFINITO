using UnityEngine;
using UnityEngine.UI;

public class PointerUI : MonoBehaviour
{
    public Image pointerImage; // L'icona del puntatore

    void Start()
    {
        if (pointerImage == null)
        {
            Debug.LogError("Pointer Image non assegnato!");
        }
    }

    void Update()
    {
        // Il puntatore UI rimane sempre al centro dello schermo
        if (pointerImage != null)
        {
            pointerImage.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        }
    }
}
