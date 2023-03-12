using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class DeerBehaviour : AnimalsBehaviour
{
    public float escapeRange;
    public float idleRange;
    public float escapeSpeed;
    
    private Vector3 targetPos;
    public bool isReachWalkPoint;
   
    private void Update()
    {
        
        if (base.IsInActiveRange(escapeRange))
        {
            this.Move();
        } else this.Idle();
    }

    public void Move()
    {
        this.SetTagetPoint(escapeRange);
        agent.speed = escapeSpeed;
        agent.SetDestination(base.targetPoint);

    }
    public void Idle()
    {
        agent.speed = 3f;
        base.SetTagetPoint(idleRange);
        agent.SetDestination(base.targetPoint);
    }

    public override void SetTagetPoint(float range)
    {
        
        base.SetTagetPoint(range); 
        if (IsReachTagetPoint())
        {
            base.targetPoint += (player.transform.forward * 30f);
        }
        
       
    }

}
