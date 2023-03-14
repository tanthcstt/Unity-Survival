using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


public class DamageReceiver : MonoBehaviour 
{
    public float health;
    public float maxHealth = 10;
   
    public virtual void Awake()
    {
        this.health = this.maxHealth;
    }
    public virtual void Receive(int damage)
    {
        this.Hurt(damage);
        Debug.Log("- " + damage + " " + gameObject.name);
        if (IsDeath(this.health))
        {
           
            Dead();
        }
    }

    public virtual bool IsDeath(float health)
    {
        if (health <= 0) return true;
        return false;        
    }

    public void Hurt(int damage)
    {
        this.health -= damage;   
    }
    public void Dead()
    {
        if (gameObject.GetComponent<BreakableObjData>())
        {    
            GameObject dropObj = gameObject.GetComponent<BreakableObjData>().generalData.DropObj;
            if (dropObj) Instantiate(dropObj, transform.position, transform.rotation);
        }
       
        gameObject.SetActive(false);
    }
}

