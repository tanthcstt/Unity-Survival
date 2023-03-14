using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WolfBehaviour : AnimalsBehaviour
{
   
    public float attackRange;
    public float patrollingRange;
    public float chasingRange;
    public float chasingSpeed = 8f;
    public int damage;
    private void Awake()
    {
        damage = gameObject.GetComponent<BreakableObjData>().generalData.damage;  
    }
    private void Update()
    {
        if (IsInActiveRange(attackRange))
        {
            Attack();
            return;
        }
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
    public void Attack()
    {   // raycast
        // send damage
        Debug.Log("attack");
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 2f, base.playerLayer))
        {
            DamageReceiver damageReceiver = hit.collider.gameObject.GetComponent<DamageReceiver>();
            if (damageReceiver != null)
            {
                DamageSender.instance.DoDamage(damage, hit.collider.gameObject);
            }
        }
    }
}
