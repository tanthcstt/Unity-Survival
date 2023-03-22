using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehaviour : WolfBehaviour
{
    public Transform bulletSpawner;

    public GameObject bulletPrefabs;
    private float reloadingTime = 1f;
    private float runTime = 0f;


    public override void Attack()
    {
        transform.LookAt(playerBody.transform);
        runTime+=Time.deltaTime;    
        if (runTime >= reloadingTime)
        {
            runTime = 0;
        } else
        {
            return;
        }

        GameObject bullet = Instantiate(bulletPrefabs, bulletSpawner.position, transform.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        
        rb.velocity = (playerBody.transform.position - bulletSpawner.transform.position) * 50f;
      
        bullet.GetComponent<Projectile>().isActive = true;
    }
}
