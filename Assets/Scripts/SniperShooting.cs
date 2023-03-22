using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperShooting : GunShooting
{
    public override void Aiming()
    {
        if (isEquiped && Input.GetMouseButtonDown(1))
        {
            base.WeaponStatus(base.aim); // aiming
            cam.fieldOfView = 10f;          
        }
        if (isEquiped && Input.GetMouseButtonUp(1))
        {
            base.WeaponStatus(base.hand); // unaiming
            cam.fieldOfView = 60f;
        }
    }
}
