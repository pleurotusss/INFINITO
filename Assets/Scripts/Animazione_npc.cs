using UnityEngine;

public class FloatingNPC : MonoBehaviour
{
    public float floatSpeed = 2f; // Velocità dell'oscillazione
    public float floatHeight = 0.2f; // Altezza massima dell'oscillazione
    public float baseHeight = 1.5f; // Altezza iniziale da terra
    private float startY;

    void Start()
    {
        startY = transform.position.y; // Memorizza la posizione iniziale
    }

    void Update()
    {
        // Calcola il nuovo valore Y con un movimento sinusoidale
        float newY = startY + baseHeight + Mathf.Sin(Time.time * floatSpeed) * floatHeight;

        // Applica la posizione aggiornata senza modificare X e Z
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
