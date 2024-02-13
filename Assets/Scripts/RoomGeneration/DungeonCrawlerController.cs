using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction {
    top = 0,
    left = 1,
    down = 2,
    right = 3,
}

//Create a Dictionary of the path and the vector, the crawler will take
public class DungeonCrawlerController : MonoBehaviour
{
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();
    private static readonly Dictionary<Direction, Vector2Int> directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.top, Vector2Int.up},
        {Direction.left, Vector2Int.left},
        {Direction.down, Vector2Int.down},
        {Direction.right, Vector2Int.right}

    };

    //Generate the dungeon depending on the number of crawlers and the min/max iterations of the rooms that should be created
    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();

        //Add the crawlers to the dungeon on the start room at the 0,0 coordinates
        for (int i = 0; i < dungeonData.numberOfCrawlers; i++)
        {
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));
        }

        //randomize the number of generated rooms
        int iterations = Random.Range(dungeonData.iterationMin, dungeonData.iterationMax);

        //move the crawlers using the movement map
        //add the position of the crawlers to the positionsVisited, to not stand on the same position
        for (int i = 0; i < iterations; i++)
        {
            foreach (DungeonCrawler dungeonCrawler in dungeonCrawlers)
            {
                Vector2Int newPos = dungeonCrawler.Move(directionMovementMap);
                positionsVisited.Add(newPos);
            }
        }

        return positionsVisited;
    }
}
