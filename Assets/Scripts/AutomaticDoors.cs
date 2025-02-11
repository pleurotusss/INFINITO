using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticDoors : MonoBehaviour
{
    public GameObject portaDestra;
    public GameObject portaSinistra;


    bool playerIsHere;

    private float maximumOpening = 0.5f;
    private float movementSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        playerIsHere = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Animazione delle porte, simile a prima
        float step = movementSpeed * Time.deltaTime;

        if (playerIsHere)
        {
            // Aprire le porte
            portaDestra.transform.localPosition = new Vector3(
                Mathf.MoveTowards(portaDestra.transform.localPosition.x, maximumOpening, step),
                portaDestra.transform.localPosition.y,
                portaDestra.transform.localPosition.z
            );

            portaSinistra.transform.localPosition = new Vector3(
                Mathf.MoveTowards(portaSinistra.transform.localPosition.x, -maximumOpening, step),
                portaSinistra.transform.localPosition.y,
                portaSinistra.transform.localPosition.z
            );
        }
        else
        {
            // Chiudere le porte
            portaDestra.transform.localPosition = new Vector3(
                Mathf.MoveTowards(portaDestra.transform.localPosition.x, 0f, step),
                portaDestra.transform.localPosition.y,
                portaDestra.transform.localPosition.z
            );

            portaSinistra.transform.localPosition = new Vector3(
                Mathf.MoveTowards(portaSinistra.transform.localPosition.x, 0f, step),
                portaSinistra.transform.localPosition.y,
                portaSinistra.transform.localPosition.z
            );
        }
    }

    public void ApriPorte()
    {
        playerIsHere = true; // Simula la presenza del giocatore per aprire le porte
    }

    public void ChiudiPorte()
    {
        playerIsHere = false; // Simula la presenza del giocatore per chiudere le porte
    }
}
