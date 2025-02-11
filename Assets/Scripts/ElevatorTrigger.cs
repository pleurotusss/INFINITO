using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    private bool _playerExited = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerExited = true;
            Debug.Log("Il giocatore è uscito dall'ascensore.");
        }
    }

    public bool HasPlayerExited()
    {
        return _playerExited;
    }
}
