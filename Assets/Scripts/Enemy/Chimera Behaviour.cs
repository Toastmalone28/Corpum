using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Pathfinding;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ChimeraBehaviour : EnemyBehaviour
{
    public float swipeCooldown = 2f;
    public float shootCooldown = 2f;
    private bool attackReady = true;
    public BossProjectile proj;

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 6 && attackReady)
        {
            animator.SetBool("swipeAttackRange", true);
            
            StartCoroutine(SwipeTimer());
        }
        else
        {
            animator.SetBool("swipeAttackRange", false);
        }

        if (distanceToPlayer >= 6 && attackReady)
        {
            animator.SetBool("shootAttack", true);

            StartCoroutine(ShootTimer());
        }
        else
        {
            animator.SetBool("shootAttack", false);
        }
    }

    IEnumerator ShootTimer()
    {
        attackReady = false;
        float timer = 0f;

        while (timer < shootCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        GameObject.Instantiate(proj, this.transform.position, this.transform.rotation);
        attackReady = true;
    }

    IEnumerator SwipeTimer()
    {
        attackReady = false;
        float timer = 0f;

        while (timer < swipeCooldown)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        attackReady = true;
    }
}
