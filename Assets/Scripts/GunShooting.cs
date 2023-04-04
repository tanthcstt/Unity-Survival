using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunShooting : Shooting
{

    private int bulletInMagazine;
    public int maxBulletsInMagazine = 2;    

    protected bool isBulletsInMagazine;
    protected float reloadTime = 2f;
    public bool isReloading;
    public override void Awake()
    {
        base.Awake();
        bulletInMagazine = maxBulletsInMagazine;
      
    }
   
    private void Update()
    {
        if (isEquiped && Input.GetMouseButtonDown(0))
        {
            if (bulletInMagazine <= 0)
            {
                isReloading = true;
            }
            if (isReloading)
            {
                return;
            }
            else
            {
                if (IsEnoughProjectile())
                {
                    bulletInMagazine--;
                    base.Shoot();
                } else
                {
                    Debug.Log("you dont have any bullets");
                }
              
            }

        }
      
       
    }
    public virtual void Aiming()
    {

    }
    private void FixedUpdate()
    {

        if (isReloading) this.ReloadProjectile(this.reloadTime, ref isReloading);
    }

    public override void ReloadProjectile(float reloadTime, ref bool isReloading)
    {
        base.ReloadProjectile(reloadTime, ref isReloading);
        if (!isReloading) bulletInMagazine = maxBulletsInMagazine;
    }
   
}
