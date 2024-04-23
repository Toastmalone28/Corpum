using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ChimeraBehaviour : EnemyBehaviour
{
    public Animator chimera;
    public float swipeCooldown = 4f;
    private bool swipeReady = true;
    // Start is called before the first frame update
    void Awake()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        enemyStats[StatsEnemies.maxHitPoints] = 200f;
        enemyStats[StatsEnemies.hitPoints] = 200f;
        chimera = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log(distanceToPlayer);
        if (distanceToPlayer < 20)
        {
            chimera.SetBool("swipeAttackRange", true);
            Debug.Log("Attack Range");
            
            StartCoroutine(SwipeTimer());
        }
        if (distanceToPlayer >= 8)
        {
            chimera.SetBool("swipeAttackRange", false);
        }
    }

    IEnumerator SwipeTimer()
    {
        swipeReady = false;
        float timer = 0f;

        while (timer < swipeCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        swipeReady = true;
    }
}
