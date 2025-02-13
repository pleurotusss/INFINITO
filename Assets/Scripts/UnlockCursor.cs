using UnityEngine;

public class UnlockCursor : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None; // Sblocca il cursore
        Cursor.visible = true; // Rende visibile il cursore
    }
}
