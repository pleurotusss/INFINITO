using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    public GameObject correctGraphic;
    public GameObject wrongGraphic;
    public float displayDuration = 2f; //durata grafica a schermo

    private Coroutine currentCoroutine;

    public void ShowGraphic(GameObject graphic)
    {
        if (graphic == null)
        {
            return;
        }

        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        currentCoroutine = StartCoroutine(ShowGraphicCoroutine(graphic));
    }

    private IEnumerator ShowGraphicCoroutine(GameObject graphic)
    {
        graphic.SetActive(true); //mostra la grafica
        yield return new WaitForSeconds(displayDuration);
        graphic.SetActive(false);
        currentCoroutine = null;
    }
}
