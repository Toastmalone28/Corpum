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
    void Start()
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
        if (distanceToPlayer < 5 && swipeReady)
        {
            chimera.SetTrigger("swipeAttack");
            swipeReady = false;
            StartCoroutine(SwipeTimer());
        }
    }

    IEnumerator SwipeTimer()
    {
        float timer = 0f;

        while (timer < swipeCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        swipeReady = true;
    }
}
