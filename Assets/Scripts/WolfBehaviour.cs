using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfBehaviour : AnimalsBehaviour
{

    public float patrollingRange;
    public float chasingRange;
    public float chasingSpeed = 8f;
    private void Update()
    {
        if (base.IsInActiveRange(chasingRange))
        {
            this.Chasing();
        }
        else this.Patrolling();
    }

    public void Patrolling()
    {
        agent.speed = 3f;
        base.SetTagetPoint(patrollingRange);
        agent.SetDestination(base.targetPoint);
    }
    public void Chasing()
    {
        agent.speed = chasingSpeed;
        agent.SetDestination(player.transform.position);
    }
}
