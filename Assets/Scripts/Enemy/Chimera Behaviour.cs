using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Pathfinding;
using UnityEngine;
using UnityEngine.ProBuilder;

public class ChimeraBehaviour : EnemyBehaviour
{
    public float swipeCooldown = 2f;
    public float shootCooldown = 2f;
    private bool attackReady = true;
    public BossProjectile proj;
    private GameObject head;
    private float startTimer = 0f;


    void Update()
    {
        if ((startTimer += Time.deltaTime) > 5f)
        {
            if (head == null)
                head = GameObject.FindWithTag("BossHead");
            Debug.Log(distanceToPlayer);
            distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < 6f && attackReady)
            {
                animator.SetTrigger("swipeAttack");

                StartCoroutine(SwipeTimer());
            }
            else
            {
                //animator.SetBool("swipeAttackRange", false);
            }

            if (distanceToPlayer >= 6f && attackReady)
            {
                animator.SetTrigger("shootAttack");

                StartCoroutine(ShootTimer());
            }
            else
            {
                //  animator.SetBool("shootAttack", false);
            }
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
