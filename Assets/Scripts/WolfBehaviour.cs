using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfBehaviour : AnimalsBehaviour
{
    public DamageSender damageSender;
    public float attackRange;
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
        if (IsInActiveRange(attackRange)) Attack();
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
    public void Attack()
    {   // raycast
        // send damage
        Debug.Log("attack");
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f))
        {
            DamageReceiver damageReceiver = hit.collider.gameObject.GetComponent<DamageReceiver>();
            if (damageReceiver != null)
            {
                damageSender.Send(hit.collider.gameObject);
            }
        }
    }
}
