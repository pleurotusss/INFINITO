using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drop : MonoBehaviour, IDropHandler
{
    private string correctValue; // Il valore corretto per questa zona di drop
    private string currentValue; // Il valore che è stato rilasciato in questa zona

    private bool isFilled = false; // Indica se la zona di drop è già occupata

    public AudioClip correctSound;  // Suono per quando il valore è corretto
    public AudioClip incorrectSound;  // Suono per quando il valore è errato

    public Image tratteggioTemperatura;
    public Image tratteggioMassa;
    public Image tratteggioCostellazione;

    // Imposta il valore corretto per questa zona di drop
    public void SetCorrectValue(string value)
    {
        correctValue = value;
        currentValue = "";  // Reset del valore corrente quando viene cambiata la stella
        isFilled = false;
    }

    // Quando un oggetto viene rilasciato in questa zona
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag == null)
        {
            Debug.LogError("Nessun oggetto trascinabile rilevato.");
            return;
        }

        Drag draggable = eventData.pointerDrag.GetComponent<Drag>();

        if (draggable == null)
        {
            Debug.LogError("Oggetto trascinabile non trovato.");
            return;
        }

        if (isFilled) return; // Se la zona è già occupata, non fare nulla

        currentValue = draggable.value;
        Debug.Log($"Dropped '{currentValue}' on {gameObject.name}, expected: '{correctValue}'");

        draggable.transform.SetParent(transform);
        draggable.transform.position = transform.position; // Posiziona il draggable nella zona di drop

        // Se il valore è corretto
        if (IsCorrect())
        {
            isFilled = true;
            draggable.DisableDrag();  // Disabilita il drag se il valore è corretto
            PlaySound(correctSound);  // Riproduce il suono di conferma

            // Disattiva il tratteggio corrispondente
            HideDropOutline();

            // Aggiungi punti per una risposta corretta
            if (DragDropManager.Instance != null)
            {
                DragDropManager.Instance.AggiornaPunteggio();  // Aggiorna il punteggio sullo schermo
            }
        }
        else
        {
            PlaySound(incorrectSound);  // Riproduce il suono di errore
            ResetDraggable(draggable);  // Ripristina l'oggetto alla sua posizione originale

            // Sottrai punti per un errore
            if (DragDropManager.Instance != null)
            {
                DragDropManager.Instance.punteggio = Mathf.Max(DragDropManager.Instance.punteggio - 10, 0);  // Sottrae 10 punti per un errore
                DragDropManager.Instance.AggiornaPunteggio();  // Aggiorna il punteggio sullo schermo
            }
        }

        // Controlla se tutti i valori sono corretti
        if (DragDropManager.Instance != null)
        {
            DragDropManager.Instance.CheckAllValues();
        }
    }

    // Metodo per verificare se il valore è corretto
    public bool IsCorrect()
    {
        return currentValue == correctValue;
    }

    // Metodo per nascondere il tratteggio corrispondente al drop corretto
    private void HideDropOutline()
    {
        if (gameObject.name.Contains("TempDropSpot"))
        {
            if (tratteggioTemperatura != null) tratteggioTemperatura.enabled = false;
        }
        else if (gameObject.name.Contains("MassDropSpot"))
        {
            if (tratteggioMassa != null) tratteggioMassa.enabled = false;
        }
        else if (gameObject.name.Contains("CostDropSpot"))
        {
            if (tratteggioCostellazione != null) tratteggioCostellazione.enabled = false;
        }
    }

    // Metodo per ripristinare i tratteggi quando si carica una nuova stella
    public void ResetTratteggi()
    {
        if (tratteggioTemperatura != null)
            tratteggioTemperatura.enabled = true;

        if (tratteggioMassa != null)
            tratteggioMassa.enabled = true;

        if (tratteggioCostellazione != null)
            tratteggioCostellazione.enabled = true;
    }

    public void Reset()
    {
        currentValue = "";
        isFilled = false;
        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);  // Rimuove l'oggetto precedente dalla zona di drop
        }
    }

    // Metodo per ripristinare l'oggetto se è stato rilasciato nella zona errata
    private void ResetDraggable(Drag draggable)
    {
        draggable.transform.position = draggable.GetComponent<Drag>().startPosition;
        draggable.transform.SetParent(draggable.GetComponent<Drag>().startParent);
        draggable.EnableDrag();  // Rende di nuovo trascinabile
    }

    // Metodo per riprodurre i suoni
    public void PlaySound(AudioClip sound)
    {
        if (sound == null)
        {
            Debug.LogError("Tentativo di riprodurre un suono nullo!");
            return;  // Evita il crash
        }

        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource non trovato sul GameObject!");
            return;
        }

        audioSource.PlayOneShot(sound);
    }
}
