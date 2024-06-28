using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;
using Random = UnityEngine.Random;

public class RoomInfo
{
    public string name;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;

    string currentWorldName = "Basement";

    RoomInfo currentLoadRoomData;

    public Room currRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public string[] possibleRooms;

    public List<Room> loadedRooms { get; } = new List<Room>();

    bool isLoadingRoom = false;
    bool spawnedBossRoom = false;
    bool spawnedItemRoom = false;
    bool updatedRooms = false;

    public bool UpdatedRooms
    {
        get { return updatedRooms; }
        private set { updatedRooms = value; }
    }

    public static event Action<Room, RoomState> OnRoomStateChanged;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //LoadRoom("Start", 0, 0);
        //LoadRoom("Empty", 1, 0);
        //LoadRoom("Empty", -1, 0);
        //LoadRoom("Empty", 0, 1);
        //LoadRoom("Empty", 0, -1);
    }

    private void Update()
    {
        UpdateRoomQueue();
    }

    void UpdateRoomQueue()
    {
        if(isLoadingRoom)
        {
            return;
        }

        if(loadRoomQueue.Count == 0)
        {
            if(!spawnedBossRoom)
            {
                StartCoroutine(SpawnBossRoom());
            }
            if (!spawnedItemRoom)
            {
                StartCoroutine(SpawnItemRoom());
            }
            else if(spawnedBossRoom && !updatedRooms) 
            { 
                foreach(Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                    room.RemoveUnconnectedWalls();
                }
                UpdateRooms();
                updatedRooms = true;
                AstarPath.active.Scan();
                GameManager.instance.UpdateGameState(GameState.running);
            }
            if(spawnedBossRoom && spawnedItemRoom && loadedRooms[loadedRooms.Count - 1].enemies.Count == 0)
            {
                UpdateRooms();
            }
            return;
        }

        currentLoadRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadRoomData));
    }

    IEnumerator SpawnItemRoom()
    {
        spawnedItemRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (spawnedBossRoom)
        {
            Room itemRoom = loadedRooms[UnityEngine.Random.Range(1, loadedRooms.Count - 1)];
            Room tempRoom = new Room(itemRoom.X, itemRoom.Y);
            Destroy(itemRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("Item", tempRoom.X, tempRoom.Y);
        }
    }

    IEnumerator SpawnBossRoom()
    {
        spawnedBossRoom = true;
        yield return new WaitForSeconds(0.5f);
        if(loadRoomQueue.Count == 0 )
        {
            Room bossRoom = loadedRooms[loadedRooms.Count - 1];
            Room tempRoom = new Room(bossRoom.X, bossRoom.Y);
            Destroy(bossRoom.gameObject);
            var roomToRemove = loadedRooms.Single(r => r.X == tempRoom.X && r.Y == tempRoom.Y);
            loadedRooms.Remove(roomToRemove);
            LoadRoom("End", tempRoom.X, tempRoom.Y);
        }
    }


    public void LoadRoom(string name, int x, int y)
    {
        if(DoesRoomExist(x, y))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = name;
        newRoomData.X = x;
        newRoomData.Y = y;

        loadRoomQueue.Enqueue(newRoomData);
    }

    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentWorldName + info.name;

        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while(!loadRoom.isDone)
        {
            if (!updatedRooms)
                GameManager.instance.UpdateGameState(GameState.loading);
            yield return null;
        }
    }

    public void RegisterRoom(Room room)
    {
        if (!DoesRoomExist(currentLoadRoomData.X, currentLoadRoomData.Y))
        {
            room.transform.position = new Vector3(
                currentLoadRoomData.X * room.Width,
                0,
                currentLoadRoomData.Y * room.Height
            );


            room.X = currentLoadRoomData.X;
            room.Y = currentLoadRoomData.Y;
            room.name = currentLoadRoomData.name + "-" + currentLoadRoomData.name + " " + room.X + ", " + room.Y;
            room.transform.parent = transform;

            isLoadingRoom = false;

            if(loadedRooms.Count == 0)
            {
                PositionController.instance.currRoom = room;
            }

            loadedRooms.Add(room);
        }
        else
        {
            Destroy(room.gameObject);
            isLoadingRoom = false;
        }
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y) != null;
    }

    public void OnPlayerEnterRoom(Room room)
    {
        PositionController.instance.currRoom = room;
        currRoom = room;

        UpdateRooms();
    }

    private void UpdateRooms()
    {
        foreach(Room room in loadedRooms)
        {
            if(currRoom != room)
            {
                EnemyBehaviour[] enemies = room.GetComponentsInChildren<EnemyBehaviour>();
                if(room.enemies != null)
                {
                    foreach(EnemyBehaviour enemy in enemies)
                    {
                        if (room.enemies.Contains(enemy) == false)
                        {
                            room.enemies.Add(enemy);
                        }
                        enemy.gameObject.SetActive(false);
                        Debug.Log("not in room");
                    }
                }
            }
            else
            {
                if (room.enemies != null)
                {
                    foreach (EnemyBehaviour enemy in room.enemies)
                    {
                        enemy.gameObject.SetActive(true);
                        Debug.Log("in room");
                    }
                }
            }
        }
    }

    public Room FindRoom (int x, int y)
    {
        return loadedRooms.Find(item => item.X == x && item.Y == y);
    }

    public string GetRandomRoom()
    {
        return possibleRooms[Random.Range(0, possibleRooms.Length)];
    }


    public void UpdateRoomState(Room room, RoomState state)
    {
        room.roomState = state;

        switch (state)
        {
            case RoomState.combat:
                HandleCombat();
                break;
            case RoomState.inactive:
                HandleInactive();
                break;
            case RoomState.cleared:
                HandleCleared();
                break;
        }

        OnRoomStateChanged?.Invoke(room, state);
    }

    private void HandleCleared()
    {
        Debug.Log("Room cleared");
    }

    private void HandleInactive()
    {

    }

    private void HandleCombat()
    {
        Debug.Log("Combat started");
    }
}
