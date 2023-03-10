using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBowShooting : Shooting
{

    protected bool isReloading;
    protected float reloadTime = 2f;
    public override void Awake()
    {
        base.Awake();        
    }
    private void Update()
    {
        if (isEquiped)
        {
            //aiming
            if (Input.GetMouseButtonDown(1)) base.WeaponStatus(base.aim);
            //unaiming
            if (Input.GetMouseButtonUp(1)) base.WeaponStatus(base.hand);
            // shoot
            if (Input.GetMouseButtonDown(0) && IsEnoughProjectile()) base.Shoot();
                      
            // reload   
            isReloading = true;
        
            
            
        }
    }
    private void FixedUpdate()
    {
        if (isReloading)
        {
            base.ReloadProjectile(reloadTime, ref isReloading);
        }
               
    }

}
