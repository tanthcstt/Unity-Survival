using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile :MonoBehaviour
{
    public bool isActive;
    public bool isDisappear;
    private bool isDamageSent;
    /// <summary>
    /// send damage if collide with receiver
    /// destroy if projectile is bullet
    /// </summary>

    public void Awake()
    {
      
        SetProjectileType();     
        
       
    }
    protected void SetProjectileType()
    {
        if (gameObject.GetComponent<GeneralItemData>().item.itemID == 10)
        {
            isDisappear = true;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isActive && (collision.gameObject.layer != LayerMask.NameToLayer("Player") && collision.gameObject.layer != LayerMask.NameToLayer("Items")))
        {
            SendDamage(collision);
            if (isDamageSent)
            {
                if (isDisappear)
                {
                    this.ProjectileDisappear();
                }
                else
                {
                    this.SetProjectileParent(collision);
                }
            }
        }
    }

    private void SendDamage(Collision collision)
    {
        if (isActive)
        {
            DamageSender.instance.DoDamage(gameObject.GetComponent<GeneralItemData>().item.itemDamage, collision.collider.gameObject);
            isActive = false;
        }
        isDamageSent = true;

    }
    private void SetProjectileParent(Collision collision) // attach arrow on ocject that hit
    {
        transform.SetParent(collision.transform);
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void ProjectileDisappear()
    {
        gameObject.SetActive(false);
    }
}
