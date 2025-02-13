using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public Image fillImage;  // Assegna "ProgressBar_Fill" in Inspector
    private float progress = 0f;  // Valore da 0 a 1

    public void SetProgress(float value)
    {
        progress = Mathf.Clamp01(value); // Limita tra 0 e 1
        fillImage.fillAmount = progress; // Aggiorna il riempimento
    }
}
