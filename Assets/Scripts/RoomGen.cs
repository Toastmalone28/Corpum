using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGen : MonoBehaviour
{
    public GameObject[,] Array1 = new GameObject[5,5];
    public GameObject[] Room;
    public GameObject spawnPoint;
    public GameObject player;
    public float spacing;
    public GameObject ai;
  
    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Array1.GetLength(0); i++)
        {
            for (int j = 0; j < Array1.GetLength(1); j++)
            {
                if (i == Array1.GetLength(0) / 2 && j == Array1.GetLength(1) / 2)
                {
                    Array1[i, j] = Instantiate(Room[0]);
                    Array1[i, j].transform.parent = spawnPoint.transform;
                    Array1[i, j].transform.localPosition = spawnPoint.transform.localPosition + new Vector3(i, 0, j) * spacing;
                    Array1[i, j].transform.Translate(65, 0, 0);
                    Array1[i, j].transform.rotation = Quaternion.identity;
                    Array1[i, j].name = "SpawnRoom";
                   // Array1[i, j].
                    player.transform.position = Array1[i, j].transform.localPosition + new Vector3(-2.5f, 7, 52.5f);
                    player.transform.rotation = Quaternion.identity;
                    player.name = "CorpumGuy";
                    Instantiate(player);
                   
                    
                }
                else
                {
                    Array1[i, j] = Instantiate(Room[Random.Range(1, Room.Length)]);
                    Array1[i, j].transform.parent = spawnPoint.transform;
                    Array1[i, j].transform.localPosition = spawnPoint.transform.localPosition + new Vector3(i, 0, j) * spacing;
                    Array1[i, j].transform.Translate(65, 0, 0);
                    Array1[i, j].transform.rotation = Quaternion.identity;
                    Array1[i, j].name = "Room_" + i + j;
                }
            }
        }
       AstarPath.active.Scan();
        ai.transform.position = Array1[2, 2].transform.localPosition + new Vector3(-7.5f, 7, 47.5f);
        ai.transform.rotation = Quaternion.identity;
        ai.name = "AI";
        Instantiate(ai);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
