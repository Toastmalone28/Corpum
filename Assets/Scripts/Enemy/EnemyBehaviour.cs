using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Dictionary<StatsEnemies, float> enemyStats;
    public GameObject player;
    public AIDestinationSetter destinationSetter;
    public float distanceToPlayer;

    private void Awake()
    {
        InitializeEnemyStats();
    }
    // Start is called before the first frame update
    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //destinationSetter.target = player.transform;

    }

    private void InitializeEnemyStats()
    {
        enemyStats = new Dictionary<StatsEnemies, float>
        {
            { StatsEnemies.maxHitPoints, 100f },
            { StatsEnemies.hitPoints, 100f },
            { StatsEnemies.armor, 1f },
            { StatsEnemies.damage, 10f }
        };
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            enemyStats[StatsEnemies.hitPoints] -= GameManager.instance.gunStats[StatsGun.damage] * 100 / (enemyStats[StatsEnemies.armor] + 100);
            if (enemyStats[StatsEnemies.hitPoints] <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
