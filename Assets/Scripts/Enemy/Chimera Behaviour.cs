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

    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        Debug.Log(distanceToPlayer);
        Debug.Log(attackReady);
        if (distanceToPlayer < 6 && attackReady)
        {
            animator.SetTrigger("swipeAttack");
            
            StartCoroutine(SwipeTimer());
        }
        else
        {
            //animator.SetBool("swipeAttackRange", false);
        }

        if (distanceToPlayer >= 6 && attackReady)
        {
            animator.SetTrigger("shootAttack");

            StartCoroutine(ShootTimer());
        }
        else
        {
          //  animator.SetBool("shootAttack", false);
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
        GameObject.Instantiate(proj);
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
