using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Explode : MonoBehaviour
{
    public int C4Damage = 100;
    public bool isTrigger;
    public float explodeTime = 5f;
    protected float runtime = 0f;

    private void Awake()
    {
        runtime = explodeTime;
    }
    private void FixedUpdate()
    {
        if (isTrigger) CountDown();
        if (runtime <= 0)
        {
            isTrigger = false;
            runtime = explodeTime;
            Explode();
           
        }    
    }

    private void CountDown()
    {
        runtime-=Time.fixedDeltaTime;
    }
    private void Explode()
    {
        Debug.Log("exploded");
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, 10f);
        foreach(Collider coll in hitCollider)
        {
            Debug.Log(coll.transform.parent);
            DamageReceiver damageReceiver = coll.gameObject.GetComponentInParent<DamageReceiver>();
            if (damageReceiver == null) continue;
            damageReceiver.Receive(C4Damage);
        }
        gameObject.SetActive(false);

    }
}
