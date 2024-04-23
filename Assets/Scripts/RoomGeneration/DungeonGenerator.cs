using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData dungeonGenerationData;
    private List<Vector2Int> dungeonRooms;

    private void Start()
    {
        //Calls the GenerateDungeon Method using the dungeonGenerationData prefab
        dungeonRooms = DungeonCrawlerController.GenerateDungeon(dungeonGenerationData);
        SpawnRooms(dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        RoomController.instance.LoadRoom("Start", 0, 0);

        //instanciates for every roomLocation an empty room at the position x and y coordinates of the roomLocation
        foreach (Vector2Int roomLocation in rooms) 
        {
            RoomController.instance.LoadRoom(RoomController.instance.GetRandomRoom(), roomLocation.x, roomLocation.y);
        }
        //AstarPath.active.Scan();
    }
}
