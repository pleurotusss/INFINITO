using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using StarterAssets;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Profiling.RawFrameDataView;

public class MaterialAssociation : MonoBehaviour
{
    public static MaterialAssociation Instance { get; private set; }
    private LevelContext _levelContext;

    private Transform holdPoint; // Punto di tenuta per il materiale
    public EventReference correctSound;
    public EventReference wrongSound;
    public float grabRange = 5f; // Distanza alla quale il materiale può essere preso
    private Transform heldMaterial; // Materiale che stiamo tenendo
    private AudioSource audioSource;
    private int originalLayer;
    private const int IgnoreRaycastLayer = 2;

    //private int correctMaterials = 0; // Contatore dei materiali corretti
    public int totalMaterials = 8; // Numero totale di materiali da posizionare
    


    public Transform Teca;
    private Dictionary<string, Transform> materialSlots = new Dictionary<string, Transform>();
    private Dictionary<Transform, Vector3> materialStartPosition = new Dictionary<Transform, Vector3>();
    private Dictionary<Transform, Quaternion> materialStartRotation = new Dictionary<Transform, Quaternion>();
    public GraphicsManager graphicsManager; // Riferimento alle grafiche

    // Nuovo suono per la raccolta del materiale
    // Nuovo suono per rimettere il materiale al suo posto
    public EventReference pickupSound;
    public EventReference putBackSound;

    void Awake()
    {
        // Implementazione Singleton per evitare duplicati
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject); // Se esiste già un'istanza, distruggi quella nuova
        //    return;
        //}

        _levelContext = FindObjectOfType<LevelContext>();

        // Trova la Main Camera
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            // Cerca il figlio chiamato "HoldPoint" nella Main Camera
            Transform foundHoldPoint = mainCamera.transform.Find("HoldPoint");
            if (foundHoldPoint != null)
            {
                holdPoint = foundHoldPoint;
                Debug.Log("HoldPoint assegnato correttamente.");
            }
            else
            {
                Debug.LogError("Errore: HoldPoint non trovato come figlio della Main Camera!");
            }
        }
        else
        {
            Debug.LogError("Errore: Main Camera non trovata nella scena!");
        }
    }

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        // Registra gli slot disponibili nella teca
        foreach (Transform slot in Teca)
        {
            materialSlots[slot.name] = slot;
        }

        // Registra la posizione iniziale e la rotazione dei materiali
        foreach (GameObject material in GameObject.FindGameObjectsWithTag("Material"))
        {
            Transform materialTransform = material.transform;
            materialStartPosition[materialTransform] = materialTransform.position;
            materialStartRotation[materialTransform] = materialTransform.rotation;

            Outline outline = material.GetComponent<Outline>();
            if (outline == null)
            {
                outline = material.AddComponent<Outline>();
                outline.OutlineColor = Color.yellow;
                outline.OutlineWidth = 5f;
            }
            outline.enabled = false;
        }
    }



   void CheckForSelectableMaterials()
    {
        // Disabilita tutti gli outline
        foreach (GameObject material in GameObject.FindGameObjectsWithTag("Material"))
        {
            Outline outline = material.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
        }

        // Se il giocatore ha già un materiale in mano, non evidenziare altri materiali
        if (heldMaterial != null) return;

        // Controlla se un oggetto è selezionabile
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, grabRange))
        {
            if (hit.collider.CompareTag("Material"))
            {
                Outline outline = hit.collider.GetComponent<Outline>();
                if (outline != null)
                {
                    outline.enabled = true; // Attiva il contorno
                }
            }
        }
    }



    void Update()
    {
        CheckForSelectableMaterials();
        if (Input.GetMouseButtonDown(0)) // Tasto sinistro per raccogliere o rilasciare normalmente
        {
            if (heldMaterial == null)
            {
                GrabMaterial();
            }
            else
            {
                DropMaterial();
            }
        }

        if (Input.GetMouseButtonDown(1)) // Tasto destro per rimettere l'oggetto al suo posto
        {
            if (heldMaterial != null)
            {
                PutMaterialBack();
            }
        }

        if (heldMaterial != null) // Se il materiale è in mano
        {
            MoveMaterial(); // Mantieni il materiale al punto di tenuta
        }
    }


    void PutMaterialBack()
    {
       if (heldMaterial != null)
    {
        Debug.Log($"Rimettendo {heldMaterial.name} nella posizione originale");

        // Ripristina la posizione e la rotazione originale
        if (materialStartPosition.ContainsKey(heldMaterial))
        {
            heldMaterial.position = materialStartPosition[heldMaterial];
            heldMaterial.rotation = materialStartRotation[heldMaterial];
        }

        ReleaseMaterial(); // Rilascia l'oggetto
    } 
    }

void GrabMaterial()
{
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    if (Physics.Raycast(ray, out RaycastHit hit, grabRange))
    {
        if (hit.collider.CompareTag("Material")) // Se l'oggetto ha il tag "Material"
        {
            heldMaterial = hit.collider.transform;
            
            Debug.Log($"Materiale raccolto: {heldMaterial.name}");

            var rb = heldMaterial.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true; // Disabilita la fisica mentre è in mano
            }

            originalLayer = heldMaterial.gameObject.layer;
            heldMaterial.gameObject.layer = IgnoreRaycastLayer;

            // Stacca il materiale dalla scena
            heldMaterial.SetParent(null);
            holdPoint.rotation = heldMaterial.rotation;
            Vector3 holdEuler = holdPoint.eulerAngles;
            holdPoint.rotation = Quaternion.Euler(holdEuler.x, 0, holdEuler.z);



            // Posiziona il materiale davanti al giocatore
            //heldMaterial.SetParent(holdPoint);
            heldMaterial.SetParent(FirstPersonController.Instance.holdPoint);

            heldMaterial.localPosition = Vector3.zero; // Posiziona al centro del holdPoint
            heldMaterial.localRotation = Quaternion.identity; // Evita rotazioni strane

                FMODUnity.RuntimeManager.PlayOneShot(pickupSound);
            }
    }
}


void DropMaterial()
{
    if (heldMaterial != null)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, grabRange))
        {
            // Verifica se il giocatore sta mirando a uno slot
            if (hit.collider.CompareTag("Slot"))
            {
                string slotName = hit.collider.transform.name;

                // Verifica se il nome del materiale corrisponde al nome dello slot
                if (heldMaterial.name == slotName)
                {
                    Debug.Log("Posizionamento corretto!");

                    // Trova la fessura corrispondente
                    Transform fessura = hit.collider.transform.Find(heldMaterial.name + "_Empty");
                    if (fessura != null)
                    {
                        heldMaterial.position = fessura.position;
                        heldMaterial.rotation = fessura.rotation;
                        heldMaterial.SetParent(fessura);
                    }
                    else
                    {
                        Debug.LogWarning($"Fessura non trovata per {heldMaterial.name}");
                    }

                        // Suona il suono corretto e mostra la grafica
                        FMODUnity.RuntimeManager.PlayOneShot(correctSound);

                        // Libera il riferimento al materiale
                        heldMaterial = null;

                    // Notifico al LevelManager lo stato del minigioco
                    _levelContext.IncrementCounterPlanetsFloor();
                    //correctMaterials++;

                    //if (correctMaterials == totalMaterials)
                    //{
                    //    Debug.Log("Tutti i materiali sono stati posizionati correttamente!");
                    //    LoadCutScene();
                    //}
                    //return;
                }
                else
                {
                    Debug.Log("Materiale errato!");
                        FMODUnity.RuntimeManager.PlayOneShot(wrongSound);

                        // Il materiale deve rimanere in mano
                        return;
                }
            }
        }

        // Se non è stato posizionato correttamente, il materiale rimane in mano
        Debug.Log("Posizionamento non valido. Il materiale resta in mano.");
    }
}

   


    void MoveMaterial()
    {
        if (heldMaterial != null)
        {
            heldMaterial.position = FirstPersonController.Instance.holdPoint.position; // Mantieni il materiale sempre davanti al giocatore
            heldMaterial.rotation = FirstPersonController.Instance.holdPoint.rotation;
        }
    }

    void ResetMaterialPosition()
    {
        if (materialStartPosition.ContainsKey(heldMaterial))
        {
            heldMaterial.position = materialStartPosition[heldMaterial];
            heldMaterial.rotation = materialStartRotation[heldMaterial];
        }
        

        ReleaseMaterial();
    }

    void ReleaseMaterial()
    {
        if (heldMaterial != null)
        {
            var rb = heldMaterial.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false; // Riabilita la fisica per il materiale
            }

            heldMaterial.gameObject.layer = originalLayer;
            heldMaterial.SetParent(null); // Rimuove il materiale dal punto di tenuta
            heldMaterial = null; // Libera il materiale



            FMODUnity.RuntimeManager.PlayOneShot(putBackSound);


        }
    }
}
