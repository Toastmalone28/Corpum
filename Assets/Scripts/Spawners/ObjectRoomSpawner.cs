using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }

    public List<GridController> grid;
    public RandomSpawner[] spawnerData;

    public GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        //grid = GetComponentInChildren<GridController>();
    }

    public void InitialiseObjectSpawning()
    {
        foreach (var gr in grid)
        {
            foreach (RandomSpawner rs in spawnerData)
            {
                SpawnObjects(rs, gr);
            }
        }

    }

    void SpawnObjects(RandomSpawner data, GridController gr)
    {
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);

        for (int i = 0; i < randomIteration; i++)
        {
            int randomPos = Random.Range(0, gr.availablePoints.Count - 1);
            if(gr.availablePoints.Count > 1)
            {
                GameObject go = Instantiate(data.spawnerData.itemToSpawn, gr.availablePoints[randomPos], Quaternion.identity, transform);
                gr.availablePoints.RemoveAt(randomPos);
            }
        }
    }
}
