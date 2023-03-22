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
    public float patrollingSpeed = 1f;
    public GameObject playerBody;
    public override void Awake()
    {
        base.Awake();   
        if (gameObject.GetComponent<BreakableObjData>())
        {
            damage = gameObject.GetComponent<BreakableObjData>().generalData.damage;
        }
        playerBody = GameObject.Find("FPP_Player/PlayerBody");
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
        agent.speed = patrollingSpeed;
        base.SetTagetPoint(patrollingRange);
        agent.SetDestination(base.targetPoint);
    }
    public void Chasing()
    {
        agent.speed = chasingSpeed;
        agent.SetDestination(player.transform.position);
    }
    public virtual void Attack()
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
