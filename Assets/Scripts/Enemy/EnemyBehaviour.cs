using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Dictionary<StatsEnemies, float> enemyStats;
    public GameObject player;
    public AIDestinationSetter destinationSetter;
    public float distanceToPlayer;

    // Start is called before the first frame update
    void Start()
    {
        InitializeEnemyStats();
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        destinationSetter.target = player.transform;
        if(enemyStats[StatsEnemies.hitPoints] <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void InitializeEnemyStats()
    {
        enemyStats = new Dictionary<StatsEnemies, float>();
        enemyStats.Add(StatsEnemies.maxHitPoints, 100f);
        enemyStats.Add(StatsEnemies.hitPoints, 100f);
        enemyStats.Add(StatsEnemies.armor, 1f);
        enemyStats.Add(StatsEnemies.damage, 10f);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            Debug.Log("bam");
            enemyStats[StatsEnemies.hitPoints] = (GameManager.instance.gunStats[StatsGun.damage] * 100) / (enemyStats[StatsEnemies.armor] + 100);
        }      
    }
}
