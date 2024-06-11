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
    private GameObject head;


    void Update()
    {
        if (head == null)
            head = GameObject.FindWithTag("BossHead");

        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
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
        Instantiate(proj.bullet, head.transform.position, Quaternion.LookRotation(Vector3.RotateTowards(transform.position, player.transform.position, 1f, 1f)), head.transform);
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
    private void OnDestroy()
    {
        GameManager.instance.UpdateGameState(GameState.victory);
    }
}
