using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public Vector3 startPosition; // Memorizza la posizione iniziale dell'oggetto
    public Transform startParent; // Memorizza la zona originale dell'oggetto
    private bool isDisabled = false; // Indica se il drag è disabilitato
    private Vector3 dragOffset; // Memorizza la distanza tra il cursore e l'oggetto durante il drag
    private Vector3 originalScale; // Memorizza la scala originale dell'oggetto

    public string value; // Il valore associato a questo oggetto (utile per la zona di drop)
    

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
        {
            Debug.LogWarning("CanvasGroup non trovato, lo aggiungo automaticamente.");
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        originalScale = rectTransform.localScale; // Salva la scala originale
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDisabled || rectTransform == null || canvasGroup == null) return;  // Se il drag è disabilitato o rectTransform/canvasGroup è null, non fare nulla
        startPosition = rectTransform.position;  // Salva la posizione originale
        startParent = transform.parent;  // Salva la zona originale (parent)

        // Calcola l'offset tra la posizione del mouse e la posizione del bottone

        canvasGroup.alpha = 0.6f;  // Rende l'oggetto semi-trasparente per indicare che è trascinato
        canvasGroup.blocksRaycasts = false;  // Impedisce che l'oggetto interagisca con altre zone di drop mentre viene trascinato
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDisabled) return;  // Se il drag è disabilitato, non fare nulla

        // Calcola la posizione del mouse nel mondo e mantieni la scala originale
        Vector3 worldPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out worldPosition);
        
        // Muove l'oggetto nel mondo senza alterare la sua scala
        rectTransform.position = worldPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (isDisabled || rectTransform == null || canvasGroup == null) return;  // Se il drag è disabilitato o rectTransform/canvasGroup è null, non fare nulla
        canvasGroup.alpha = 1f;  // Rende l'oggetto completamente visibile
        canvasGroup.blocksRaycasts = true;  // Riabilita l'interazione con le zone di drop

        // Verifica se il bottone è stato rilasciato dentro il Canvas
        if (IsInsideCanvas())
        {
            // Se sì, riporta il bottone alla posizione originale senza cambiare la sua scala
            rectTransform.localScale = originalScale;
            rectTransform.position = startPosition;
        }
        else
        {
            // Se il bottone è fuori dal Canvas, puoi decidere di mantenere la posizione attuale o riposizionarlo
            if (transform.parent == startParent)
            {
                rectTransform.position = startPosition;
            }
        }
    }

    // Funzione per determinare se il bottone è dentro il Canvas
    private bool IsInsideCanvas()
    {
        RectTransform canvasRect = transform.root.GetComponentInChildren<Canvas>().GetComponent<RectTransform>();
        return canvasRect.rect.Contains(canvasRect.InverseTransformPoint(rectTransform.position));
    }

    // Disabilita la possibilità di spostare l'oggetto (utile quando l'oggetto è stato rilasciato correttamente)
    public void DisableDrag()
    {
        isDisabled = true;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = false;  // Disabilita ulteriori interazioni con l'oggetto
    }

    // Abilita nuovamente il drag
    public void EnableDrag()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}
