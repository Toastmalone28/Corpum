using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class WormBehaviour : EnemyBehaviour
{
    public Animator worm;

    // Start is called before the first frame update
    void Start()
    {
        destinationSetter = GetComponent<AIDestinationSetter>();
        worm = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 5)
        {
            worm.SetBool("inAttackRange", true);
        }
        else
        {
            worm.SetBool("inAttackRange", false);
        }
    }
}
