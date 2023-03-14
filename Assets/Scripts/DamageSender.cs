using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageSender :MonoBehaviour
{
    public static DamageSender instance;    
    public Transform cam;
    // send damage to damage receiver obj
    protected int damage;

    public virtual void Awake()
    {
        instance = this;      
        LoadComponent();        
        
    }
    public void LoadComponent()
    {
        cam = GameObject.Find("FPP_Player/Main Camera").transform;
    }

    public void DoDamage(int damage, GameObject objToSend)
    {
        SetDamage(damage);
        Send(objToSend);
    }

    protected virtual void Send(GameObject objToSend)
    {
        Debug.Log(objToSend);   
        DamageReceiver receiver = objToSend.transform.GetComponentInParent<DamageReceiver>();
       
        if (receiver == null) return;
        
        receiver.Receive(damage);       
    }

    protected virtual void SetDamage(int damage)
    {
        this.damage = damage;
    }

 
}
