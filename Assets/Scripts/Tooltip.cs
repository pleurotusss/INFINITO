using UnityEngine;

public class Tooltip : MonoBehaviour
{
    public int tooltipID; // ID del pannello tooltip da mostrare

    private void OnMouseEnter()
    {
        Debug.Log("Mostro Tooltip " + tooltipID);
        TooltipManager._instance.SetAndShowTooltip(tooltipID);
    }

    private void OnMouseExit()
    {
        Debug.Log("Nascondo Tooltip " + tooltipID);
        TooltipManager._instance.HideTooltip(tooltipID);
    }
}
