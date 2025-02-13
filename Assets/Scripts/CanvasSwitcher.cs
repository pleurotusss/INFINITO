using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject welcomeCanvas;   // Canvas di benvenuto
    public GameObject creationCanvas;  // Canvas per la creazione della stella
    public GameObject fusionCanvas;    // Canvas per la fusione
    public GameObject endCanvas;       // Canvas per la fine della simulazione
    
    public Button startButton;         // Bottone per iniziare la simulazione dal benvenuto
    public Button nextButton;          // Bottone per passare alla fase successiva
    public Button endButton;           // Bottone per terminare la simulazione
    public Button ricominciaButton;    // Bottone per ricominciare la simulazione

    public Slider heliumSlider;        // Slider per l'elio
    public Slider ironSlider;          // Slider per il ferro
    public Slider hydrogenSlider;      // Slider per l'idrogeno

    private void Start()
    {
        // Imposta la visibilità iniziale dei canvas
        if (welcomeCanvas != null) welcomeCanvas.SetActive(true);
        if (creationCanvas != null) creationCanvas.SetActive(false);
        if (fusionCanvas != null) fusionCanvas.SetActive(false);
        if (endCanvas != null) endCanvas.SetActive(false);

        // Assegna gli eventi ai bottoni
        if (startButton != null) startButton.onClick.AddListener(SwitchToCreation);
        if (nextButton != null) nextButton.onClick.AddListener(SwitchToFusion);
        if (endButton != null) endButton.onClick.AddListener(SwitchToEnd);
        if (ricominciaButton != null) ricominciaButton.onClick.AddListener(SwitchToWelcome);

        if (fusionCanvas != null)
        {
            hydrogenSlider = fusionCanvas.transform.Find("hydrogenSlider")?.GetComponent<Slider>();
            heliumSlider = fusionCanvas.transform.Find("heliumSlider")?.GetComponent<Slider>();
            ironSlider = fusionCanvas.transform.Find("ironSlider")?.GetComponent<Slider>();
        }
    }

    private void SwitchToCreation()
    {
        // Passa dal canvas di benvenuto a quello di creazione
        welcomeCanvas.SetActive(false);
        creationCanvas.SetActive(true);
    }

    private void SwitchToFusion()
    {   
        ResetFusionCanvas();
        creationCanvas.SetActive(false);
        fusionCanvas.SetActive(true);
    }

    private void SwitchToEnd()
    {
        creationCanvas.SetActive(false);
        fusionCanvas.SetActive(false);
        endCanvas.SetActive(true);
    }

    public void SwitchToWelcome()
    {
        // Cancella tutti i dati salvati e torna al canvas di benvenuto
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save(); 

        Debug.Log("[CanvasSwitcher] Tutti i dati sono stati cancellati. Nuovo inizio.");
        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.UnloadSceneAsync("Minigioco_2").completed += (AsyncOperation op) =>
        {
            SceneManager.LoadSceneAsync("Minigioco_2", LoadSceneMode.Additive);
        };
    }

    private void ResetFusionCanvas()
    {
        // Resetta i campi di input nel canvas di fusione
        InputField[] inputFields = fusionCanvas.GetComponentsInChildren<InputField>();
        foreach (InputField inputField in inputFields)
        {
            inputField.text = string.Empty;
        }

        // Resetta gli slider e disabilita quelli di elio e ferro
        Slider[] sliders = fusionCanvas.GetComponentsInChildren<Slider>();
        foreach (Slider slider in sliders)
        {
            slider.value = 0;
        }

        if (heliumSlider != null) SetSliderState(heliumSlider, false);
        if (ironSlider != null) SetSliderState(ironSlider, false);
    }

    private void SetSliderState(Slider slider, bool state)
    {
        slider.interactable = state;
        Color color = state ? Color.white : new Color(0.5f, 0.5f, 0.5f, 0.5f);

        if (slider.fillRect != null)
            slider.fillRect.GetComponent<Image>().color = color; 
        if (slider.handleRect != null)
            slider.handleRect.GetComponent<Image>().color = color;
    }
}