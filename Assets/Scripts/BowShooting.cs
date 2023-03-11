using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class BowShooting : Shooting
{
    
    private bool isStartShoot;


    private void Update()
    {
        if (isEquiped)
        {
            if (isEquiped && Input.GetMouseButtonDown(0))
            {
                this.WeaponStatus(base.aim); // aiming
            }
           
            if (Input.GetMouseButtonDown(0))
            {               
                     isStartShoot = true;                                       

            }
            if (isStartShoot && Input.GetMouseButtonUp(0))
            {
                // if is enough arr ==> shoot
                if (base.IsEnoughProjectile())
                {
                   
                    base.Shoot();
                }
                else
                {
                    Debug.Log("You don't have any arrows");
                }


                isStartShoot = false;
                if (isEquiped && Input.GetMouseButtonUp(0))
                {
                    this.WeaponStatus(base.hand); // unaiming
                }
            }

        }
    }

    public override void WeaponStatus (Transform status)
    {
        if (status == base.hand)
        {
            base.WeaponStatus(status);
         /*   cam.fieldOfView = 60f;*/
        }
        else
        {

            this.transform.SetParent(status);
            this.gameObject.transform.position = status.position + (transform.right * 0.1f);
            this.transform.rotation = status.rotation;
            this.gameObject.transform.Rotate(0f, 0f, -20f);
            /*cam.fieldOfView = 45f;*/
        }
       
    }



}
