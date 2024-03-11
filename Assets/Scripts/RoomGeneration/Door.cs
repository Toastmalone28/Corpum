using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public enum DoorType
    {
        left, right, top, bottom
    }

    public DoorType doorType;
    public Room room;

    private void Start()
    {

        room = GetComponentInParent<Room>();

        // �berpr�fen
        if (room == null)
        {
            Debug.LogError("Door script is not attached to a GameObject with a Room script.");
        }
    }

    private void Update()
    {
        // �berpr�fen, ob alle Gegner im Raum tot sind
        if (room != null && room.AllEnemiesDead())
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        // Gegebenfalls animation
        gameObject.SetActive(false);
    }
}