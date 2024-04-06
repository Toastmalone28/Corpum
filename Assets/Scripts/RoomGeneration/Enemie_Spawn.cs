using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public int enemyCount;

    private void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        // Nur spawnen, wenn noch keine Gegner gespawnt wurden
        if (enemyCount == 0)
        {
            for (int i = 0; i < 4; i++)
            {
                // Zufällige Positionen im Bereich (x: 25-40, z: 26-36)
                float xPos = Random.Range(25, 40);
                float zPos = Random.Range(26, 36);

                GameObject spawnedEnemy = Instantiate(enemy, new Vector3(xPos, 2, zPos), Quaternion.identity);
                spawnedEnemy.GetComponent<EnemyBehaviour>().Start();
                yield return new WaitForSeconds(0.2f);

                // Informiere den Raum über den gespawnten Gegner
                Room currentRoom = GetComponent<Room>();
                if (currentRoom != null)
                {
                    //currentRoom.AddEnemy(spawnedEnemy);
                }
            }

            // Alle 4 Gegner wurden gespawnt
            enemyCount = 4;
        }
    }
}