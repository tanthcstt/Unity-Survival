using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour  
{
    private int damage;
    public int health = 20;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Items") && collision.gameObject.GetComponent<GeneralItemData>())
        {
            //check if this obj do damage
            if (collision.gameObject.GetComponent<GeneralItemData>().item.isDoDamage)
            {
                damage = collision.gameObject.GetComponent<GeneralItemData>().item.itemDamage;

                TakeDam(damage);
            }
            

        }
    }
    
    private void TakeDam(int damage)
    {
        health -= damage;
        Debug.Log("-" + damage);    
        if (health <= 0)
        {
           Death(); 
        }

    }

    private void Death()
    {
        Destroy(transform.gameObject);
    }

    
 
}
