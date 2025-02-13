using UnityEngine;
using TMPro;

public class StarEnd : MonoBehaviour
{
    public TextMeshProUGUI resultText;  // Testo per la descrizione della stella
    public TextMeshProUGUI starTypeText; // Nuovo testo per il tipo di stella
    public GameObject starModel; // Riferimento al modello della stella
    public Material NanaBiancaMaterial; // Materiale per cambiare il colore della stella
    public Material GiganteRossaMaterial; // Materiale per cambiare il colore della stella
    public Material SupernovaMaterial; // Materiale per cambiare il colore della stella

    private float mass;
    private float hydrogen;
    private float helium;
    private float iron;

    public GameObject star;
    private float floatSpeed = 0.5f;  // Velocità della fluttuazione
    private float floatAmplitude = 0.5f;  // Ampiezza della fluttuazione
    private Vector3 initialPosition;



    private void Start()
    {
        initialPosition = starModel.transform.position;

        if (PlayerPrefs.HasKey("StarLifespan"))
        {
            float lifespan = PlayerPrefs.GetFloat("StarLifespan"); // Durata finale calcolata
            DetermineStarEnd(lifespan);

        }
        else
        {
            Debug.LogWarning("[StarEnd] Durata della stella non trovata in PlayerPrefs");
        }
    }

    private void Update()
    {
        float rotationSpeed = Mathf.Lerp(5f,50f,Mathf.InverseLerp(3900f,33000f,500000f));

        starModel.transform.Rotate(Vector3.up * rotationSpeed* Time.deltaTime);

        float floatOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        starModel.transform.position = initialPosition + new Vector3(0, floatOffset,0);
    }

    private void DetermineStarEnd(float lifespan)
    {
        string result = "";
        string starType = ""; // Variabile per il tipo di stella

        // Determina il tipo di stella alla fine della sua vita
        if (lifespan < 1f)
        {
            starType = "Supernova"; 
            result += "La tua stella ha avuto una vita breve e intensa, terminata con una spettacolare esplosione in una supernova.";
            ChangeStarModel("Supernova"); 
        }
        else if (lifespan < 10f)
        {
            starType = "Gigante Rossa";
            result += "La tua stella ha vissuto una vita simile al nostro Sole. Dopo aver consumato l'idrogeno, si è trasformata in una gigante rossa. Il suo nucleo residuo diventerà una nana bianca."; 
            ChangeStarModel("Gigante Rossa"); 
        }
        else
        {
            starType = "Nana Bianca";
            result += "La tua stella ha avuto una vita lunghissima, spegnendosi lentamente e trasformandosi in una nana bianca fredda e densa.";
            ChangeStarModel("Nana Bianca"); 
        }

        starTypeText.text = starType; // Imposta il nome del tipo di stella
        resultText.text = result; // Imposta la descrizione
    }

    public void ChangeStarModel(string starType)
    {
        Renderer starRenderer = starModel.GetComponent<Renderer>();

        // Cambia la scala e il colore della stella in base al tipo
        switch (starType)
        {
            case "Supernova":
                starModel.transform.localScale = new Vector3(8f, 8f, 8f); 
                starRenderer.material = SupernovaMaterial;
                break;
            case "Gigante Rossa":
                starModel.transform.localScale = new Vector3(4f, 4f, 4f); 
                starRenderer.material = GiganteRossaMaterial; 
                break;
            case "Nana Bianca":
                starModel.transform.localScale = new Vector3(1f, 1f, 1f); 
                starRenderer.material = NanaBiancaMaterial; 
                break;
            default:
                starModel.transform.localScale = new Vector3(1f, 1f, 1f); 
                starRenderer.material = NanaBiancaMaterial; 
                break;
        }
    }
}
