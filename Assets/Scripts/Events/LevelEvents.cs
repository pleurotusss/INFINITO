using UnityEngine;
using UnityEngine.Events;

public class LevelEvents : ScriptableObject
{
    // Aggiungi un riferimento a PlayerEvents
    public PlayerEvents playerEvents;

    private bool isPositionSaved = false;
    private Vector3 savedPosition;

    public UnityAction<int> OnLevelChangeRequested;

    // Metodo per richiedere il cambio di livello
    public void RequestLevelChange(int floorIndex)
    {
        Debug.Log($"Sono in LevelEvents.RequestLevelChange: {floorIndex}");
        // Esegui l'evento quando viene chiamato
        OnLevelChangeRequested.Invoke(floorIndex);
    }

    // Aggiungi il metodo per verificare se una posizione è stata salvata
    public bool HasSavedPosition()
    {
        return isPositionSaved;
    }

    // Aggiungi il metodo per ottenere la posizione salvata
    public Vector3 GetSavedPlayerPosition()
    {
        return savedPosition;
    }

    // Metodo che salva la posizione (da chiamare quando necessario)
    public void SavePlayerPosition(Vector3 position)
    {
        savedPosition = position;
        isPositionSaved = true;
    }

    // Metodo che può essere usato per chiamare direttamente il salvataggio della posizione tramite PlayerEvents
    public void SavePositionViaEvents(Vector3 position)
    {
        playerEvents.SavePlayerPosition(position);  // Utilizza il playerEvents per invocare l'evento
    }
}
