using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : DamageSender
{
    public bool isActive;
    public bool isDisappear;
    private bool isDamageSent;
    /// <summary>
    /// send damage if collide with receiver
    /// destroy if projectile is bullet
    /// </summary>

    public override void Awake()
    {
        base.Awake();
        this.SetProjectileType();     
        
       
    }
    protected void SetProjectileType()
    {
        if (this.GetComponent<GeneralItemData>().item.itemID == 10)
        {
            isDisappear = true;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (isActive && (collision.gameObject.layer != LayerMask.NameToLayer("Player") && collision.gameObject.layer != LayerMask.NameToLayer("Items")))
        {
            this.SendDamage(collision);
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
            base.Send(collision.collider.gameObject);
            isActive = false;
        }
        isDamageSent = true;

    }
    private void SetProjectileParent(Collision collision) // attach arrow on ocject that hit
    {
        this.transform.SetParent(collision.transform);
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void ProjectileDisappear()
    {
        this.gameObject.SetActive(false);
    }
}
