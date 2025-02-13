using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarFusion : MonoBehaviour
{
    public TextMeshProUGUI lifespanText;
    public Slider hydrogenSlider;
    public Slider heliumSlider;
    public Slider ironSlider;

    private float initialLifespan; // Durata iniziale salvata in StarCreation
    private float hydrogen;
    private float helium;
    private float iron;
    private bool hydrogenModified = false;
    public GameObject star;

    private float floatSpeed = 0.5f;  // Velocità della fluttuazione
    private float floatAmplitude = 0.5f;  // Ampiezza della fluttuazione
    private Vector3 initialPosition;


    private void Start()
{
    initialPosition = star.transform.position;
    // Recupera la durata iniziale della stella salvata in StarCreation
    initialLifespan = PlayerPrefs.GetFloat("StarLifespan") * 10f; // Conversione in miliardi di anni
    Debug.Log($"[StarFusion] Durata iniziale recuperata: {initialLifespan} miliardi di anni");

    // Disattiva inizialmente gli slider di elio e ferro
    SetSliderState(heliumSlider, false);
    SetSliderState(ironSlider, false);

    UpdateFusion();
}


    private void Update()
    {
        float rotationSpeed = Mathf.Lerp(5f,50f,Mathf.InverseLerp(3900f,33000f,50000f));

        star.transform.Rotate(Vector3.up * rotationSpeed* Time.deltaTime);

        float floatOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        star.transform.position = initialPosition + new Vector3(0, floatOffset,0);
        // Aggiorna il valore dell'idrogeno
        float newHydrogen = hydrogenSlider.value;

        // Controlla se l'idrogeno è stato modificato
        if (!hydrogenModified && newHydrogen != hydrogen)
        {
            hydrogenModified = true;
            SetSliderState(heliumSlider, true);
            SetSliderState(ironSlider, true);
        }

        hydrogen = newHydrogen;
        helium = heliumSlider.value;
        iron = ironSlider.value;

        Debug.Log($"[StarFusion] Valori slider - Idrogeno: {hydrogen:F2}, Elio: {helium:F2}, Ferro: {iron:F2}");
        UpdateFusion();
    }

    private void UpdateFusion()
    {
        // Calcola la nuova durata della stella in base agli elementi
        float adjustedLifespan = CalculateAdjustedLifespan(initialLifespan, hydrogen, helium, iron);

        // Verifica se la durata è inferiore a 1 miliardo di anni, e convertila in milioni di anni
        string lifespanDisplay;
        if (adjustedLifespan < 1f)
        {
            // Converte in milioni di anni se la durata è inferiore a 1 miliardo
            float lifespanInMillions = adjustedLifespan * 1000; // Converte in milioni di anni
            lifespanDisplay = $"{lifespanInMillions:F0} milioni di anni"; // Mostra senza decimali
        }
        else
        {
            lifespanDisplay = $"{adjustedLifespan:F2} miliardi di anni"; // Mostra con due decimali
        }

        // Aggiorna il testo della durata stimata
        lifespanText.text = $"{lifespanDisplay}";

        Debug.Log($"[StarFusion] Durata stimata aggiornata: {lifespanDisplay}");

        // Salva i valori per eventuale recupero futuro
        PlayerPrefs.SetFloat("hydrogen", hydrogen);
        PlayerPrefs.SetFloat("helium", helium);
        PlayerPrefs.SetFloat("iron", iron);
    }

    private float CalculateAdjustedLifespan(float initialLifespan, float hydrogen, float helium, float iron)
    {
        // Effetto degli elementi sulla durata stimata (percentuali ponderate)
        float adjustmentFactor = Mathf.Max(0.1f, (0.7f * hydrogen - 0.2f * helium - 0.1f * iron)); // Evita valori negativi
        return initialLifespan * adjustmentFactor;
    }

    private void SetSliderState(Slider slider, bool state)
    {
        slider.interactable = state;
        Color color = state ? Color.white : new Color(0.5f, 0.5f, 0.5f, 0.5f); // Grigio scuro con trasparenza

        if (slider.fillRect != null)
            slider.fillRect.GetComponent<Image>().color = color;
        if (slider.handleRect != null)
            slider.handleRect.GetComponent<Image>().color = color;
    }
}
