using UnityEngine;
using System.Collections.Generic;

public class DragDropManager : MonoBehaviour
{
    public static DragDropManager Instance;

    public Drop temperaturaDrop;
    public Drop massaDrop;
    public Drop costellazioneDrop;
    public GameObject starModel;  
    public TMPro.TextMeshProUGUI starNameText; 

    public GameObject CanvasPunteggioZero;  // Canvas che appare quando il punteggio è 0
    public GameObject CanvasInizio;  // Canvas che appare quando il punteggio è 0



    public GameObject TempButtonPrefab;    // Bottone rosso per la temperatura
    public GameObject MassButtonPrefab; // Bottone viola per la massa
    public GameObject CostButtonPrefab;   // Bottone blu per la costellazione

    public Transform temperaturaContainer;  // Contenitore per i bottoni della temperatura
    public Transform massaContainer;        // Contenitore per i bottoni della massa
    public Transform costellazioneContainer; // Contenitore per i bottoni della costellazione

    private List<Stella> stelle = new List<Stella>();  
    private List<Stella> stelleDisponibili = new List<Stella>(); 
    private Stella stellaCorrente; 

    public GameObject CanvasGioco;  // Canvas per il gioco

    public GameObject CanvasFine;  // Canvas per la fine del gioco
    private bool gameOver = false;  

    public TMPro.TextMeshProUGUI starProgressText;
    public TMPro.TextMeshProUGUI scoreText; // Testo per visualizzare il punteggio
    public TMPro.TextMeshProUGUI finalScoreText; // Testo per visualizzare il punteggio finale


    private int stelleCorrette = 0;
    public int punteggio = 100; // Iniziamo con 100 punti

    private List<string> valoriTemperaturaUsati = new List<string>();
    private List<string> valoriMassaUsati = new List<string>();
    private List<string> valoriCostellazioneUsati = new List<string>();


    private void Awake()
    {
        
        
        Instance = this;  
        starModel.SetActive(false);  

        stelle.Add(new Stella("Stella Polare", "7410K", "1.8M", "Orsa Minore"));
        stelle.Add(new Stella("Proxima Centauri", "3042K", "0.12M", "Centauro"));
        stelle.Add(new Stella("Betelgeuse", "3500K", "11.6M", "Orione"));

        ResetStelleDisponibili();
        SelezionaNuovaStella();
        AggiornaProgressoStelle();
        AggiornaPunteggio(); // Aggiorna il punteggio all'avvio
    }


    public void AvviaGioco()
    {
        CanvasInizio.SetActive(false);  // Disattiva il Canvas di inizio
        CanvasGioco.SetActive(true);  // Attiva il Canvas di gioco
    }
    

    public void AggiornaProgressoStelle()
    {
        starProgressText.text = $"{stelleCorrette}/{stelle.Count}";
    }

  public void AggiornaPunteggio()
{
    scoreText.text = $"{punteggio}";  // Aggiorna il testo del punteggio
    PlayerPrefs.SetInt("Punteggio", punteggio); 
    PlayerPrefs.Save(); 

    if (punteggio <= 0)
    {
        punteggio = 0; // Evita punteggi negativi
        AttivaCanvasPunteggioZero();  // Chiama la funzione per attivare il Canvas speciale
    }
}

private void FineGiocoCanvas()
{
    gameOver = true;
    CanvasGioco.SetActive(false);  // Disattiva il Canvas di gioco
    CanvasFine.SetActive(true);  // Attiva il Canvas di fine gioco

    starNameText.gameObject.SetActive(false);
    temperaturaDrop.gameObject.SetActive(false);
    massaDrop.gameObject.SetActive(false);
    costellazioneDrop.gameObject.SetActive(false);

    int punteggioFinale = PlayerPrefs.GetInt("Punteggio", 0);
    if (finalScoreText != null)
    {
        finalScoreText.text = $"{punteggioFinale}";
    }

    AggiornaProgressoStelle();
    //PlayerPrefs.SetInt("GiocoCompletato", 1);
    //PlayerPrefs.Save();
}






    


    private void AttivaCanvasPunteggioZero()
{
  
    gameOver = true;

    CanvasGioco.SetActive(false); // Nasconde il gioco
    CanvasPunteggioZero.SetActive(true); // Mostra il canvas punteggio zero



    // Disattiva gli elementi di gioco
    starNameText.gameObject.SetActive(false);
    temperaturaDrop.gameObject.SetActive(false);
    massaDrop.gameObject.SetActive(false);
    costellazioneDrop.gameObject.SetActive(false);

    
}


    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))  // Modifica questa combinazione di tasti come preferisci
        {
            AttivaCheat();
        }
    }

    private void AttivaCheat()
    {
        FineGiocoCanvas();
        PlayerPrefs.SetInt("Punteggio", 100);  // Imposta il punteggio a 100
        PlayerPrefs.Save();
    }



    private void ResetStelleDisponibili()
    {
        stelleDisponibili = new List<Stella>(stelle);
    }

    private List<string> GeneraOpzioni(string valoreCorretto, List<string> possibiliValori, List<string> valoriUsati)
    {
        List<string> opzioni = new List<string>(possibiliValori);
        opzioni.Remove(valoreCorretto);

        // Rimuovi i valori già usati
        opzioni.RemoveAll(v => valoriUsati.Contains(v));

        opzioni = opzioni.GetRange(0, Mathf.Min(2, opzioni.Count));  // Assicurati di avere almeno 2 opzioni
        opzioni.Add(valoreCorretto);
        opzioni.Sort((a, b) => Random.Range(-1, 2));  // Mischia le opzioni

        return opzioni;
    }

    private void GeneraOggettiTrascinabili()
    {
        List<string> opzioniTemperatura = GeneraOpzioni(stellaCorrente.temperatura, new List<string> { "5000K", "3000K", "7000K", "2000K", "4000K", stellaCorrente.temperatura }, valoriTemperaturaUsati);
        List<string> opzioniMassa = GeneraOpzioni(stellaCorrente.massa, new List<string> { "0.5M", "1.5M", "2.0M", "3.0M", "2.5", stellaCorrente.massa }, valoriMassaUsati);
        List<string> opzioniCostellazione = GeneraOpzioni(stellaCorrente.costellazione, new List<string> { "Orsa Maggiore", "Orione", "Cigno", "Boh", "Bah", stellaCorrente.costellazione }, valoriCostellazioneUsati);

        // Crea bottoni per ogni categoria nei rispettivi contenitori
        CreaDraggables(opzioniTemperatura, TempButtonPrefab, temperaturaContainer);
        CreaDraggables(opzioniMassa, MassButtonPrefab, massaContainer);
        CreaDraggables(opzioniCostellazione, CostButtonPrefab, costellazioneContainer);
    }

    private void CreaDraggables(List<string> opzioni, GameObject prefab, Transform container)
    {
        foreach (string opzione in opzioni)
        {
            GameObject draggable = Instantiate(prefab, container);
            Drag dragScript = draggable.GetComponent<Drag>();
            dragScript.value = opzione;

            TMPro.TextMeshProUGUI textComponent = draggable.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = opzione;
            }
        }
    }

    public void SelezionaNuovaStella()
{
    if (gameOver) return;  

    if (stelleDisponibili.Count == 0)
    {
        FineGioco();
        return;
    }

    int index = Random.Range(0, stelleDisponibili.Count);
    stellaCorrente = stelleDisponibili[index];  
    stelleDisponibili.RemoveAt(index);  

    starNameText.text = stellaCorrente.nome;

    temperaturaDrop.SetCorrectValue(stellaCorrente.temperatura);
    massaDrop.SetCorrectValue(stellaCorrente.massa);
    costellazioneDrop.SetCorrectValue(stellaCorrente.costellazione);

    temperaturaDrop.Reset();
    massaDrop.Reset();
    costellazioneDrop.Reset();

    // Resetta i tratteggi di tutti i drop
    foreach (Drop dropZone in FindObjectsOfType<Drop>())
    {
        dropZone.ResetTratteggi();
    }

    foreach (Transform child in temperaturaContainer) Destroy(child.gameObject);
    foreach (Transform child in massaContainer) Destroy(child.gameObject);
    foreach (Transform child in costellazioneContainer) Destroy(child.gameObject);

    GeneraOggettiTrascinabili();
    starModel.SetActive(true);  
}

    

   private void FineGioco()
{
    gameOver = true;
    if (CanvasFine != null)
    {
        CanvasFine.SetActive(true);
    }

    starNameText.gameObject.SetActive(false);
    temperaturaDrop.gameObject.SetActive(false);
    massaDrop.gameObject.SetActive(false);
    costellazioneDrop.gameObject.SetActive(false);

    // Recupera e mostra il punteggio finale
    int punteggioFinale = PlayerPrefs.GetInt("Punteggio", 0);
    if (finalScoreText != null)
    {
        finalScoreText.text = $"{punteggioFinale}";
    }

    AggiornaProgressoStelle();
    PlayerPrefs.SetInt("GiocoCompletato", 1);  // Imposta il flag per indicare che il gioco è stato completato
    PlayerPrefs.Save();
}


    public void CheckAllValues()
    {
        if (temperaturaDrop.IsCorrect() && massaDrop.IsCorrect() && costellazioneDrop.IsCorrect())
        {
            Debug.Log("Hai completato la stella: " + stellaCorrente.nome);
            stelleCorrette++;

            // Aggiungi i valori corretti alle liste di valori usati
            valoriTemperaturaUsati.Add(stellaCorrente.temperatura);
            valoriMassaUsati.Add(stellaCorrente.massa);
            valoriCostellazioneUsati.Add(stellaCorrente.costellazione);

            AggiornaProgressoStelle();
            AggiornaPunteggio(); // Aggiorna il punteggio
            Invoke("SelezionaNuovaStella", 2f);
        }
        else
        {
            Debug.Log("Errore nell'associazione della stella: " + stellaCorrente.nome);
            AggiornaPunteggio(); // Aggiorna il punteggio
        }
    }

    public void ResetGioco()
{
    gameOver = false;
    stelleCorrette = 0; 
    punteggio = 100; 
    
    AggiornaProgressoStelle();
    AggiornaPunteggio();
    
    ResetStelleDisponibili(); 

    valoriTemperaturaUsati.Clear();
    valoriMassaUsati.Clear();
    valoriCostellazioneUsati.Clear();

    // Resetta i drop per cancellare i dati salvati della partita precedente
    temperaturaDrop.Reset();
    massaDrop.Reset();
    costellazioneDrop.Reset();

    // Resetta i tratteggi di tutti i drop
    foreach (Drop dropZone in FindObjectsOfType<Drop>())
    {
        dropZone.ResetTratteggi();
    }

    starModel.SetActive(false);
    starNameText.gameObject.SetActive(true);

    temperaturaDrop.gameObject.SetActive(true);
    massaDrop.gameObject.SetActive(true);
    costellazioneDrop.gameObject.SetActive(true);

    foreach (Transform child in temperaturaContainer) Destroy(child.gameObject);
    foreach (Transform child in massaContainer) Destroy(child.gameObject);
    foreach (Transform child in costellazioneContainer) Destroy(child.gameObject);

    SelezionaNuovaStella();

    if (CanvasFine != null)
    {
        CanvasFine.SetActive(false);
    }
}




    

    [System.Serializable]
    public class Stella
    {
        public string nome;
        public string temperatura;
        public string massa;
        public string costellazione;

        public Stella(string nome, string temperatura, string massa, string costellazione)
        {
            this.nome = nome;
            this.temperatura = temperatura;
            this.massa = massa;
            this.costellazione = costellazione;
        }
    }
}
