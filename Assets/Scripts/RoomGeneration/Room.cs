using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int Width;
    public int Height;

    public int X;
    public int Y;

    private bool updatedDoors = false;

    public Room(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public List<Door> doors = new List<Door>();

    public Wall leftWall;
    public Wall rightWall;
    public Wall topWall;
    public Wall bottomWall;

    public List<Wall> walls = new List<Wall>();

    // Enemies in the room
    public List<EnemyBehaviour> enemies = new List<EnemyBehaviour>();
    public RoomState roomState;

    private void Awake()
    {
        RoomController.OnRoomStateChanged += RoomControllerOnRoomStateChanged;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (RoomController.instance == null)
        {
            return;
        }

        Door[] ds = GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            doors.Add(d);
            switch (d.doorType)
            {
                case Door.DoorType.right:
                    rightDoor = d;
                    break;
                case Door.DoorType.left:
                    leftDoor = d;
                    break;
                case Door.DoorType.top:
                    topDoor = d;
                    break;
                case Door.DoorType.bottom:
                    bottomDoor = d;
                    break;
            }
        }

        Wall[] ws = GetComponentsInChildren<Wall>();
        foreach (Wall w in ws)
        {
            walls.Add(w);
            switch (w.wallType)
            {
                case Wall.WallType.right:
                    rightWall = w;
                    break;
                case Wall.WallType.left:
                    leftWall = w;
                    break;
                case Wall.WallType.top:
                    topWall = w;
                    break;
                case Wall.WallType.bottom:
                    bottomWall = w;
                    break;
            }
        }

        // gives the room a name and coordinates, and transforms it using the width and height
        RoomController.instance.RegisterRoom(this);
    }

    private void Update()
    {
        if (name.Contains("End") && !updatedDoors)
        {
            RemoveUnconnectedWalls();
            RemoveUnconnectedDoors();
            updatedDoors = true;
        }
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.right:
                    if (GetRight() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                        door.gameObject.SetActive(false);
                    break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                        door.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public void RemoveUnconnectedWalls()
    {
        foreach (Wall wall in walls)
        {
            switch (wall.wallType)
            {
                case Wall.WallType.right:
                    if (GetRight() != null)
                        wall.gameObject.SetActive(false);
                    break;
                case Wall.WallType.left:
                    if (GetLeft() != null)
                        wall.gameObject.SetActive(false);
                    break;
                case Wall.WallType.top:
                    if (GetTop() != null)
                        wall.gameObject.SetActive(false);
                    break;
                case Wall.WallType.bottom:
                    if (GetBottom() != null)
                        wall.gameObject.SetActive(false);
                    break;
            }
        }
    }

    public Room GetRight()
    {
        if (RoomController.instance.DoesRoomExist(X + 1, Y))
        {
            return RoomController.instance.FindRoom(X + 1, Y);
        }
        return null;
    }
    public Room GetLeft()
    {
        if (RoomController.instance.DoesRoomExist(X - 1, Y))
        {
            return RoomController.instance.FindRoom(X - 1, Y);
        }
        return null;
    }
    public Room GetTop()
    {
        if (RoomController.instance.DoesRoomExist(X, Y + 1))
        {
            return RoomController.instance.FindRoom(X, Y + 1);
        }
        return null;
    }
    public Room GetBottom()
    {
        if (RoomController.instance.DoesRoomExist(X, Y - 1))
        {
            return RoomController.instance.FindRoom(X, Y - 1);
        }
        return null;
    }

    void OnDrawGizmos()
    {
        // indicates the width and height for better visibility
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + new Vector3(Width / 2, 0, Height / 2), new Vector3(Width, 0, Height));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
            RoomController.instance.UpdateRoomState(this, RoomState.combat);
        }
    }
    private void RoomControllerOnRoomStateChanged(RoomState state)
    {

    }
}