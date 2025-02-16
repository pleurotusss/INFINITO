using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using TMPro.Examples;

public class PlanetsFloorState : LevelState
{
    public PlanetsFloorState(LevelContext context, LevelStateMachine.ELevelStateMachine estate) : base(context, estate)
    {
        Context = context;
    }

    private LevelStateMachine.ELevelStateMachine _nextState;
    private bool stopPointFound = false;
    private bool hasPlayedIntroDialogue = false;
    private bool levelCompletedDialoguePlayed = false;
    private float dialogueDuration = 5f; // Durata in secondi per ogni dialogo

    private Dictionary<string, string[]> _dialogues = new Dictionary<string, string[]>()
    {
        { "intro", new string[] {
            "Eccoci arrivati al piano sui pianeti! Intorno a te ci sono tutti e 8 i pianeti.",
            "Sopra il tavolo troverai delle sostanze da associare a ogni pianeta, posizionali nel vano sotto a ogni piedistallo."
        }},
        { "hint", new string[] { "Intorno a te cerca altre informazioni sul sistema solare, per facilitarti il lavoro!" } },
        { "level_completed", new string[] { "Complimenti! Hai completato questa sfida. Sei pronto per la prossima?" } },
        { "already_completed", new string[] { "Hai già completato questo piano. Procedi al prossimo!" } },
        { "leds_completed", new string[] { "Hai completato tutti i piani. Torna nell'atrio a goderti lo spettacolo!" } }
    };

    public override void EnterState()
    {
        Context.StartRoutine(FindStopPointAfterDelay());

        Debug.Log($"COUNTER LED: {Context.GameContext.CounterLED}");

        if (Context.GameContext.CounterLED >= 3) 
        { 
            ShowDialogue("leds_completed");
            levelCompletedDialoguePlayed = true;
            return;
        }

        if (Context.CounterPlanetsFloor >= 8)
        {
            ShowDialogue("already_completed");
            levelCompletedDialoguePlayed = true;
            return;
        }

    }

    public override void UpdateState()
    {
        if (!stopPointFound) return; // Aspetta che lo StopPoint venga trovato prima di muoversi

        if (Context.CounterPlanetsFloor < 8)
        {
            Context.SetMustFollowPlayer(false);
        }
        else
        {
            Context.SetMustFollowPlayer(true);
        }

        if (Context.MustFollowPlayer && Context.Target != null)
        {
            float distance = Vector3.Distance(Context.transform.position, Context.Target.position);

            if (distance > Context.StopDistance)
            {
                Context.transform.position = Vector3.MoveTowards(Context.transform.position, Context.Target.position, Context.MoveSpeed * Time.deltaTime);
            }

            RotateTowards(Context.Target.position);
        }
        else
        {
            Context.transform.position = Vector3.MoveTowards(Context.transform.position, Context.StopPoint.position, Context.MoveSpeed * Time.deltaTime);
            RotateTowards(Context.StopPoint.position);

            if (!hasPlayedIntroDialogue && Vector3.Distance(Context.transform.position, Context.StopPoint.position) < 0.1f)
            {
                Context.StartRoutine(PlayIntroDialogue());
                hasPlayedIntroDialogue = true;
            }
        }

        // Se tutti i piani sono stati completati, mostra il dialogo "leds_completed"
        if (Context.GameContext.CounterLED >= 3 && !levelCompletedDialoguePlayed)
        {
            ShowDialogue("leds_completed");
            levelCompletedDialoguePlayed = true;
            return;
        }

        if (Context.CounterPlanetsFloor >= 8 && !levelCompletedDialoguePlayed && Context.GameContext.CounterLED < 3)
        {
            ShowDialogue("level_completed");
            levelCompletedDialoguePlayed = true;
        }

        if (Input.GetKeyDown(KeyCode.E) && Vector3.Distance(Context.transform.position, Context.Target.position) < 3f && Context.CounterPlanetsFloor < 8)
        {
            ShowDialogue("hint");
        }
    }

    public override void ExitState() { }

    private IEnumerator FindStopPointAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        GameObject stopPoint = null;

        while (stopPoint == null)
        {
            stopPoint = GameObject.FindWithTag("NPCStopPoint");

            if (stopPoint == null)
            {
                Debug.LogWarning("NPCStopPoint ancora non trovato! Riprovo...");
                yield return new WaitForSeconds(0.5f);
            }
        }

        Context.SetStopPoint(stopPoint.transform);
        stopPointFound = true;
        Debug.Log("NPCStopPoint trovato!");
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - Context.transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Context.transform.rotation = Quaternion.Slerp(Context.transform.rotation, lookRotation, Context.RotationSpeed * Time.deltaTime);
        }
    }

    private void ShowDialogue(string dialogueKey)
    {
        if (_dialogues.ContainsKey(dialogueKey))
        {
            Context.StartRoutine(DisplayDialogue(_dialogues[dialogueKey]));
        }
    }

    private IEnumerator DisplayDialogue(string[] dialogueLines)
    {
        Context.DialogueBox.SetActive(true); // Attiva la UI del dialogo

        foreach (var line in dialogueLines)
        {
            Context.DialogueText.text = line; // Mostra il testo attuale
            yield return new WaitForSeconds(dialogueDuration); // Attendi il tempo specificato
        }

        Context.DialogueBox.SetActive(false); // Disattiva la UI del dialogo alla fine
    }


    private IEnumerator PlayIntroDialogue()
    {
        yield return DisplayDialogue(_dialogues["intro"]);
    }

    public override LevelStateMachine.ELevelStateMachine GetNextState()
    {
        switch (Context.GameContext.CurrentFloor)
        {
            case "Piano_-1":
                Debug.Log("LEVEL MANAGER --- STANZA PIANETI");
                _nextState = LevelStateMachine.ELevelStateMachine.PlanetsLevel;
                break;
            case "Piano-2":
                Debug.Log("LEVEL MANAGER --- STANZA STELLE");
                _nextState = LevelStateMachine.ELevelStateMachine.StarsLevel;
                break;
            case "Piano_-3":
                Debug.Log("LEVEL MANAGER --- STANZA RELATIVITÀ");
                _nextState = LevelStateMachine.ELevelStateMachine.RelativityLevel;
                break;
            default:
                Debug.Log("LEVEL MANAGER --- NESSUN LIVELLO ATTIVO");
                _nextState = LevelStateMachine.ELevelStateMachine.StartState;
                break;
        }

        return _nextState;
    }
}