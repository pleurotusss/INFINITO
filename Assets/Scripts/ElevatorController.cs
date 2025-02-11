//using UnityEngine;
//using System.Collections; // NECESSARIO per IEnumerator
//using UnityEngine.SceneManagement;


//public class ElevatorController : MonoBehaviour
//{
//    public LevelEvents levelEvents;
//    public CameraShake cameraShake;
//    public AutomaticDoors porte;

//    private bool isMoving = false;

//    public void SelectFloor(int floorIndex)
//    {
//        if (!isMoving)
//        {
//            StartCoroutine(MoveElevator(floorIndex));
//        }
//    }
//    public void OpenElevator()
//    {
//        if (porte != null)
//        {
//            porte.ApriPorte();
//        }
//    }

//    public void CloseElevator()
//    {
//        if (porte != null)
//        {
//            porte.ChiudiPorte();
//        }
//    }
//    IEnumerator MoveElevator(int floorIndex)
//    {
//        Debug.Log($"Sono in MoveElevator: {floorIndex}");
//        isMoving = true;

//        CloseElevator();

//        yield return new WaitForSeconds(1.5f);

//        if (cameraShake != null)
//            yield return StartCoroutine(cameraShake.Shake(2f, 0.1f));

//        yield return new WaitForSeconds(2f);

//        // Invia la richiesta di cambio livello
//        levelEvents.RequestLevelChange(floorIndex);

//        OpenElevator();

//        isMoving = false;

//        yield return null;
//    }

   

//}
