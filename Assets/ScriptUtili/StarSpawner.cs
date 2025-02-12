using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject starPrefab;  // Prefab della stella
    public float spawnInterval = 1f; // Intervallo di spawn
    public float minSize = 0.05f; // Dimensione minima
    public float maxSize = 0.2f; // Dimensione massima

    void Start()
    {
        // Inizia il ciclo di spawn
        InvokeRepeating("SpawnStar", 0f, spawnInterval);
    }

    void SpawnStar()
    {
        // Crea una stella alla posizione del sistema di particelle
        GameObject newStar = Instantiate(starPrefab, transform.position, Random.rotation);
        newStar.transform.localScale = Vector3.one * Random.Range(minSize, maxSize);  // Imposta dimensioni casuali
    }
}
