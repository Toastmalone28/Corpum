using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ChimeraBehaviour : EnemyBehaviour
{
    public float swipeCooldown = 2f;
    private bool swipeReady = true;

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 6 && swipeReady)
        {
            animator.SetBool("swipeAttackRange", true);
            
            StartCoroutine(SwipeTimer());
        }
        if (distanceToPlayer >= 6)
        {
            animator.SetBool("swipeAttackRange", false);
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
