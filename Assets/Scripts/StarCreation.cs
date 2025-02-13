using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StarCreation : MonoBehaviour
{
    public GameObject starModel;  // Modello della stella (oggetto 3D)
    public TextMeshProUGUI starTypeText;
    public TextMeshProUGUI massText;
    public TextMeshProUGUI temperatureText;
    public TextMeshProUGUI lifespanText;
    public Slider temperatureSlider;

    public Material blueStarMaterial;
    public Material lightBlueStarMaterial;
    public Material whiteBlueStarMaterial;
    public Material whiteYellowStarMaterial;
    public Material yellowStarMaterial;
    public Material orangeStarMaterial;
    public Material redStarMaterial;

    private float temperature;
    private float mass;

    private float floatSpeed = 0.5f;  // Velocità della fluttuazione
    private float floatAmplitude = 0.5f;  // Ampiezza della fluttuazione
    private Vector3 initialPosition;



    private void Start()
    {
        temperature = temperatureSlider.value;
        initialPosition = starModel.transform.position;
        UpdateStar();
    }

    private void Update()
    {
        temperature = temperatureSlider.value;
        UpdateStar();

        float rotationSpeed = Mathf.Lerp(5f,50f,Mathf.InverseLerp(3900f,33000f,500000f));

        starModel.transform.Rotate(Vector3.up * rotationSpeed* Time.deltaTime);

        float floatOffset = Mathf.Sin(Time.time * floatSpeed) * floatAmplitude;
        starModel.transform.position = initialPosition + new Vector3(0, floatOffset,0);
    }

    private void UpdateStar()
    {
        mass = CalculateMass(temperature);
        UpdateStarAppearance(temperature, mass);
        UpdateStarType(temperature);

        float lifespan = CalculateInitialLifespan(mass);
        PlayerPrefs.SetFloat("StarLifespan", lifespan);
        PlayerPrefs.Save();
        
        Debug.Log($"[StarCreation] Durata iniziale calcolata e salvata: {lifespan:F2} miliardi di anni");

        massText.text = mass.ToString("F2") + " Masse solari";
        temperatureText.text = temperature.ToString("F0") + " K";
        UpdateLifespanDisplay(lifespan);

        PlayerPrefs.SetFloat("mass", mass);
        PlayerPrefs.SetString("starTypeText", starTypeText.text);
    }

    private void UpdateLifespanDisplay(float lifespan)
    {
        string lifespanDisplay = lifespan < 1f ? $"{(lifespan * 1000):F0} milioni di anni" : $"{lifespan:F2} miliardi di anni";
        lifespanText.text = lifespanDisplay;
    }

    private float CalculateInitialLifespan(float mass)
    {
        if (mass <= 0.1f) mass = 0.1f;
        return 10f / Mathf.Pow(mass, 2.5f);
    }

    private float CalculateMass(float temperature)
    {
        if (temperature >= 33000f) return Mathf.Lerp(16f, 50f, Mathf.InverseLerp(33000f, 100000f, temperature));
        if (temperature >= 10000f) return Mathf.Lerp(2.1f, 16f, Mathf.InverseLerp(10000f, 33000f, temperature));
        if (temperature >= 7300f) return Mathf.Lerp(1.4f, 2.1f, Mathf.InverseLerp(7300f, 10000f, temperature));
        if (temperature >= 6000f) return Mathf.Lerp(1.04f, 1.4f, Mathf.InverseLerp(6000f, 7300f, temperature));
        if (temperature >= 5300f) return Mathf.Lerp(0.8f, 1.04f, Mathf.InverseLerp(5300f, 6000f, temperature));
        if (temperature >= 3900f) return Mathf.Lerp(0.45f, 0.8f, Mathf.InverseLerp(3900f, 5300f, temperature));
        return 0.45f;
    }

    private void UpdateStarAppearance(float temperature, float mass)
    {
        Renderer starRenderer = starModel.GetComponent<Renderer>();
        if (starRenderer != null)
        {
            if (temperature >= 33000f) starRenderer.material = blueStarMaterial;
            else if (temperature >= 10000f) starRenderer.material = lightBlueStarMaterial;
            else if (temperature >= 7300f) starRenderer.material = whiteBlueStarMaterial;
            else if (temperature >= 6000f) starRenderer.material = whiteYellowStarMaterial;
            else if (temperature >= 5300f) starRenderer.material = yellowStarMaterial;
            else if (temperature >= 3900f) starRenderer.material = orangeStarMaterial;
            else starRenderer.material = redStarMaterial;
        }

        float normalizedMass = Mathf.InverseLerp(0.2f, 16f, mass);
        float scale = Mathf.Lerp(1f, 10f, normalizedMass);
        starModel.transform.localScale = new Vector3(scale, scale, scale);
    }

    private void UpdateStarType(float temperature)
    {
        if (temperature >= 33000f) starTypeText.text = "Stella Blu";
        else if (temperature >= 10000f) starTypeText.text = "Stella Blu chiaro";
        else if (temperature >= 7300f) starTypeText.text = "Stella Bianco-Azzurra";
        else if (temperature >= 6000f) starTypeText.text = "Stella Bianco-Gialla";
        else if (temperature >= 5300f) starTypeText.text = "Stella Gialla";
        else if (temperature >= 3900f) starTypeText.text = "Stella Arancione";
        else starTypeText.text = "Stella Rossa";
    }
}
