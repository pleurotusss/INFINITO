using UnityEngine;
using UnityEngine.Assertions;

public class InterazioneBottone : MonoBehaviour
{
    private Camera mainCamera;
    public float detectionDistance = 3f; // Distanza massima per premere il bottone
    public LayerMask buttonLayer; // Layer dei bottoni per ottimizzare il Raycast

    private ElevatorStateMachine _elevatorStateMachine;
    private ElevatorContext _elevatorContext;

    void Start()
    {
        mainCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _elevatorStateMachine = FindObjectOfType<ElevatorStateMachine>();
        if (_elevatorStateMachine != null)
            _elevatorContext = _elevatorStateMachine.GetContext();
        else
            Debug.LogError("ElevatorStateMachine non trovata nella scena.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetMouseButtonDown(0)) // Click sinistro del mouse
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit, detectionDistance, buttonLayer))
            {
                Debug.Log("Raycast colpisce: " + hit.collider.gameObject.name);

                // Bottone esterno: apre le porte e cambia stato a Arrived
                if (hit.collider.CompareTag("BottoneAscensore"))
                    _elevatorContext.SetOpenButtonPressed(true);

                // Bottone interno: seleziona piano e cambia stato a Moving
                else if (hit.collider.CompareTag("BottoniChiusura"))
                {
                    int floorIndex;
                    if (int.TryParse(hit.collider.gameObject.name, out floorIndex))
                        _elevatorContext.SetNextFloor(floorIndex);
                    
                }
            }
        }
    }
}
