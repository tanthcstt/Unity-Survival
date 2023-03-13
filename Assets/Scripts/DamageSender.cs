using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DamageSender :MonoBehaviour
{
   
    public Transform cam;
    // send damage to damage receiver obj
    protected int damage;

    public virtual void Awake()
    {
        LoadComponent();        
        SetDamage();
        
    }
    public void LoadComponent()
    {
        cam = GameObject.Find("FPP_Player/Main Camera").transform;
    }
    public virtual void Send(GameObject objToSend)
    {
        Debug.Log(objToSend);   
        DamageReceiver receiver = objToSend.transform.GetComponentInParent<DamageReceiver>();
       
        if (receiver == null) return;
        
        receiver.Receive(damage);       
    }

    public virtual void SetDamage()
    {
        this.damage = gameObject.GetComponent<GeneralItemData>().item.itemDamage;
    }

 
}
