using UnityEngine;
using UnityEngine.Events;

public class PlayerEvents : ScriptableObject
{
    public UnityAction<Vector3> OnPlayerPositionSaved;

    private Vector3 savedPosition;
    private bool positionSaved = false;

    public void SavePlayerPosition(Vector3 position)
    {
        savedPosition = position;
        positionSaved = true;
        OnPlayerPositionSaved?.Invoke(position);  // Invoca l'evento con la posizione
    }

    public bool HasSavedPosition()
    {
        return positionSaved;
    }

    public Vector3 GetSavedPlayerPosition()
    {
        return savedPosition;
    }
}
