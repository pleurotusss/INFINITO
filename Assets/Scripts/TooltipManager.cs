using System.Collections;
using UnityEngine;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager _instance;

    public GameObject[] tooltips; // Array dei Pannelli Tooltip
    public float tooltipDelay = 0.3f;
    private Coroutine showTooltipCoroutine;

    void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        // Disattiva tutte le tooltip all'inizio
        foreach (GameObject tooltip in tooltips)
        {
            tooltip.SetActive(false);
        }
    }

    public void SetAndShowTooltip(int tooltipID)
    {
        if (showTooltipCoroutine != null)
            StopCoroutine(showTooltipCoroutine);

        if (tooltipID < 0 || tooltipID >= tooltips.Length)
            return; // Se l'ID è fuori range, non fare nulla

        // Disattiva tutte le altre tooltip prima di attivare quella giusta
        foreach (GameObject tooltip in tooltips)
        {
            tooltip.SetActive(false);
        }

        tooltips[tooltipID].SetActive(true);
        showTooltipCoroutine = StartCoroutine(ShowTooltipWithDelay(tooltips[tooltipID]));
    }

    private IEnumerator ShowTooltipWithDelay(GameObject tooltip)
    {
        yield return new WaitForSeconds(tooltipDelay);
        tooltip.SetActive(true);
    }

    public void HideTooltip(int tooltipID)
    {
        if (showTooltipCoroutine != null)
            StopCoroutine(showTooltipCoroutine);

        if (tooltipID < 0 || tooltipID >= tooltips.Length)
            return;

        tooltips[tooltipID].SetActive(false);
    }
}
