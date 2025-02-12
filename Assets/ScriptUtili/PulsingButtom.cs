using UnityEngine;

public class PulsingButton : MonoBehaviour
{
    public RectTransform buttonTransform; // Riferimento al bottone
    public float pulseSpeed = 2f;         // Velocità del pulsare
    public float pulseAmount = 0.1f;      // Ampiezza del pulsare

    private Vector3 originalScale;

    void Start()
    {
        // Salva la scala originale del bottone
        originalScale = buttonTransform.localScale;
    }

    void Update()
    {
        // Fai pulsare il bottone
        PulsateButton();
    }

    void PulsateButton()
    {
        // Calcola il fattore di pulsazione
        float scaleFactor = 1 + Mathf.Sin(Time.time * pulseSpeed) * pulseAmount;

        // Applica la scala modificata
        buttonTransform.localScale = originalScale * scaleFactor;
    }
}
