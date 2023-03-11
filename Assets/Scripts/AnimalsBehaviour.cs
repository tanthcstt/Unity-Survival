using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimalsBehaviour : MonoBehaviour
{
    public float activeRange;

    public Vector3 targetPoint;
    public Transform centerPoint;
    public bool isSetTargetPoint;
    public LayerMask playerLayer;
    public GameObject player;
    private void Awake()
    {
        player = GameObject.Find("FPP_Player");
    }
    private void Start()
    {
        centerPoint = transform;
    }
  

    public virtual void SetTagetPoint(float range)
    {
        if (isSetTargetPoint && !IsReachTagetPoint()) return;      
        float randomX = Random.Range(-range, range);
        float randomZ = Random.Range(-range, range);
        targetPoint = new Vector3(centerPoint.position.x + randomX, transform.position.y, centerPoint.position.z + randomZ);
        isSetTargetPoint = true;
    }
 
    public bool IsReachTagetPoint()
    {
        Vector3 distanceToWalkPoint = targetPoint - centerPoint.position;
        distanceToWalkPoint.y = 0;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            
            isSetTargetPoint = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsInActiveRange(float range)
    {
        if (Physics.CheckSphere(transform.position, range, playerLayer))
        {
            return true;
        }
        return false;
    }
}
