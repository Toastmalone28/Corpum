using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class WormBehaviour : EnemyBehaviour
{

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 5)
        {
            animator.SetBool("inAttackRange", true);
        }
        else
        {
            animator.SetBool("inAttackRange", false);
        }
    }
}
