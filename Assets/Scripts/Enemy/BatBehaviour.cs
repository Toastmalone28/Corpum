using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Pathfinding;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.ProBuilder;

public class BatBehaviour : EnemyBehaviour
{
    public GameObject projectile;
    public Animator bat;
    public GameObject head;
    private bool isAttacking;
    private float shootTime = 1f;

    IEnumerator AttackTimer()
    {
        float timer = 0f;

        while (timer < shootTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        isAttacking = false;
    }

    // Start is called before the first frame update
    void Start()
    {
     
        destinationSetter = GetComponent<AIDestinationSetter>();
        bat = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 16)
        {
            bat.SetBool("inAttackRange", true);
            if (!isAttacking)
            {
                GameObject.Instantiate(projectile, head.transform.position, head.transform.rotation);
                isAttacking = true;
                StartCoroutine(AttackTimer());
            }
                
        }
        else
        {
            bat.SetBool("inAttackRange", false);
        }
    }
}
