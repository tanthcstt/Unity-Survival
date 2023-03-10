using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnermyBehaviour : MonoBehaviour
{
    public NavMeshAgent agent;
    public float patrollingRange;
    public float chasingRange;
    public float attackRange;
    public float minDistance;
    public float patrolSpeed;
    public float chasingSpeed;

    public bool isInChasingRange;
    public bool isSetWalkPoint;
    public bool isReachWalkPoint;
    public bool isInAttackRange;

    public LayerMask playerLayer;
    private Transform playerTransform;
    public Vector3 walkPoint;
    private Transform center;

    private void Start()
    {
        playerTransform = GameObject.Find("FPP_Player").transform;
        center = transform;
    }
    private void Update()
    {
        isInChasingRange = Physics.CheckSphere(transform.position,chasingRange, playerLayer);
        isInAttackRange = Physics.CheckSphere(transform.position,attackRange, playerLayer);
        if (isInChasingRange)
        {
            Chasing();
        } else if (isInAttackRange)
        {
            Attack();
        } else
        {
            Patrolling();
        }

    }
    public void Patrolling()
    {
        agent.speed = 5f;
        // set walk point
        // if have walkpoint ==> move to walkpoint
        //check if reach walkpoint
        // set new walkpoint
        if(isSetWalkPoint)
        {
            ReachWalkPoint();
            if (isReachWalkPoint)
            {
                SetPatrollingWalkPoint();
            } else
            {
                agent.speed = patrolSpeed;
                agent.SetDestination(walkPoint);
            }
        } else
        {
            SetPatrollingWalkPoint();
        }
    }

    public void Chasing()
    {
        agent.speed = chasingSpeed;
        agent.SetDestination(playerTransform.position);
    }

    public void Attack()
    {
        
    }

    public void SetPatrollingWalkPoint()
    {
        float randomX = Random.Range(- patrollingRange, patrollingRange);
        float randomZ = Random.Range(-patrollingRange, patrollingRange);
        walkPoint = new Vector3(center.position.x + randomX, transform.position.y, center.position.z + randomZ);
        isSetWalkPoint = true;
      
    }
    
    public void ReachWalkPoint()
    {
        Vector3 distanceToWalkPoint = walkPoint - center.position;
        distanceToWalkPoint.y = 0;
       
        if (distanceToWalkPoint.magnitude < 1f)
        {
            isReachWalkPoint = true;
        } else
        {
            isReachWalkPoint = false;   
        }
    }
}
