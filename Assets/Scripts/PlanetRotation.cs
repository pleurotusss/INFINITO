using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetRotation : MonoBehaviour
{
    [Header("Rotazione sul proprio asse")]
    [Tooltip("Periodo di rotazione in ore terrestri.")]
    public float rotationPeriodHours;

    [Tooltip("Inclinazione dell'asse di rotazione in gradi.")]
    public float axialTilt;

    [Tooltip("Velocità minima per i pianeti più lenti.")]
    public float minRotationSpeed = 5f;

    [Tooltip("Velocità massima per i pianeti più veloci.")]
    public float maxRotationSpeed = 50f;

    private float adjustedRotationSpeed;
    private Transform rotationAxisObject;

    void Start()
    {
        // Ottieni il nome del pianeta e rimuovi "_Pianeta"
        string planetName = gameObject.name.Replace("_Pianeta", "");

        switch (planetName)
        {
            case "Mercurio": rotationPeriodHours = 1407.6f; axialTilt = 0.034f; break;
            case "Venere": rotationPeriodHours = 5832.5f; axialTilt = 177.3f; break;
            case "Terra": rotationPeriodHours = 23.56f; axialTilt = 23.4f; break;
            case "Marte": rotationPeriodHours = 24.6f; axialTilt = 25.2f; break;
            case "Giove": rotationPeriodHours = 9.93f; axialTilt = 3.1f; break;
            case "Saturno": rotationPeriodHours = 10.7f; axialTilt = 26.7f; break;
            case "Urano": rotationPeriodHours = 17.2f; axialTilt = 97.8f; break;
            case "Nettuno": rotationPeriodHours = 16.1f; axialTilt = 28.3f; break;
            default:
            Debug.LogWarning("Pianeta non riconosciuto: " + planetName);
            return;
        }

        // Normalizza la velocità di rotazione
        if (rotationPeriodHours > 0)
        {
            float rawSpeed = 1f / rotationPeriodHours;
            adjustedRotationSpeed = Mathf.Lerp(minRotationSpeed, maxRotationSpeed, rawSpeed / 0.1f);
        }

        // Creiamo un oggetto invisibile per inclinare l'asse di rotazione
        rotationAxisObject = new GameObject(gameObject.name + "_Axis").transform;
        rotationAxisObject.position = transform.position;
        rotationAxisObject.rotation = Quaternion.Euler(axialTilt, 0, 0); // Inclina l'asse

        // Sposta il nuovo oggetto nella stessa scena del pianeta
        SceneManager.MoveGameObjectToScene(rotationAxisObject.gameObject, gameObject.scene);

        // Imposta il parent
        transform.parent = rotationAxisObject; // Il pianeta ruoterà attorno a questo oggetto
    }

    void Update()
    {
        if (rotationPeriodHours > 0)
        {
            rotationAxisObject.Rotate(Vector3.up, adjustedRotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
