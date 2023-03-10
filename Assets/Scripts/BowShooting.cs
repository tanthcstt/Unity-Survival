using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowShooting : Shooting
{
    
    private bool isStartShoot;


    private void Update()
    {
        if (isEquiped)
        {
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
            }

        }
    }


   
  

}
