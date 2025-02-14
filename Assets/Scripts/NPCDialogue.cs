using UnityEngine;
using TMPro; // Libreria per TextMeshPro

public class NPCDialogue : MonoBehaviour
{
    public string[] dialogues;  // Dialoghi iniziali
    public string hint;  // Dialogo fisso dopo il primo ciclo
    private string empty = ""; // Per accendere e spegnere il dialogo di HINT 
    private bool hintSwitch = false;
    public string completedLevel;
    public string completedGame;
    private int currentDialogueIndex = 0;
    //private bool isPlayerNearby = false;
    private bool finishedInitialDialogues = false; // Flag per controllare i dialoghi
    //private bool playerHasLeft = false; // Controlla se il giocatore è uscito dall'area

    public GameObject dialogueBox;  // Riferimento al box del dialogo
    public TextMeshProUGUI dialogueText; // Testo del dialogo
    private Camera mainCamera;

    
    void Start()
    {
        dialogueBox.SetActive(false); // Nasconde il box all'inizio
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click sinistro del mouse
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // Se l'oggetto cliccato è quello con lo script
                {
                    ShowDialogue();
                }
            }
        }
        //if (isPlayerNearby && Input.GetKeyDown(KeyCode.Space))
        //{
        //    ShowDialogue();
        //}
    }

    private void ShowDialogue()
    {
        dialogueBox.SetActive(true); // Mostra il dialogo

        if (finishedInitialDialogues)
        {
            if(hintSwitch)
                dialogueText.text = hint; // Ora può dire il quarto dialogo
            else 
                dialogueText.text = empty;

            hintSwitch = !hintSwitch;
        }
        else
        {
            // Mostra i dialoghi normali
            dialogueText.text = dialogues[currentDialogueIndex];
            currentDialogueIndex++;

            // Se abbiamo finito i dialoghi iniziali
            if (currentDialogueIndex >= dialogues.Length)
            {
                finishedInitialDialogues = true; // Passa alla fase del quarto dialogo
        }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        isPlayerNearby = true;

    //        // Se il giocatore ha già fatto i primi 3 dialoghi e si è allontanato, può sentire il quarto
    //        if (finishedInitialDialogues && playerHasLeft)
    //        {
    //            dialogueBox.SetActive(true);
    //            dialogueText.text = hint;
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        isPlayerNearby = false;
    //        dialogueBox.SetActive(false); // Nasconde il dialogo quando il player si allontana

    //        // Se il giocatore aveva finito i primi 3 dialoghi, attiva la possibilità di sentire il quarto
    //        if (finishedInitialDialogues)
    //        {
    //            playerHasLeft = true;
    //        }
    //    }
    //}
}

