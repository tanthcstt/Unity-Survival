using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float shootingRange;
    protected int amountToCheck = 1;
    public bool isEquiped;
    protected float reloadRunTime;
    private Transform projectileSpawnerTrans;

    public GeneralItemData projectileType;
    //shooting data
    protected List<GeneralItemData> requiredProjectile = new List<GeneralItemData>();
    protected GameObject projectilePrefab;
    protected Transform aim;
    protected Transform hand;
    public Camera cam;  


    // get bullet or arrow
    // get damage from bullet / arr
    // instantiate blt/arr
    // make bullet / arr fly

    public virtual void Awake()
    {
        this.LoadComponent();
        this.SetProjectileType();
    }

    private void SetProjectileType()
    {
        requiredProjectile.Add(projectileType);
    }
    public void Shoot()
    {      
        this.ProjectileFly();      
    }

    private void ProjectileFly()
    {
        projectileSpawnerTrans = this.transform.Find("BulletSpawner");
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnerTrans.position, transform.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        /*     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);*/

        rb.velocity = transform.forward * shootingRange;
 
        projectile.GetComponent<Projectile>().isActive = true;
       // set projective can do damage

    }

    
    protected bool IsEnoughProjectile()
    {
        requiredProjectile[0].itemCount = amountToCheck; // after check if isenough = true ==> itemfilter set element.itemCount of requiredlist = 0
        if (ItemFilter.Instance.IsEnoughItems(requiredProjectile))
        {
            return true;
        }
        return false;
    }
    private void LoadComponent()
    {
       
        // projectile
        projectilePrefab = this.GetComponent<GeneralItemData>().item.arrows_bullets[0];
        projectileType = projectilePrefab.GetComponent<GeneralItemData>();
        // aiming transform
        aim = GameObject.Find("FPP_Player/Main Camera/AimPosition").transform;
        hand = GameObject.Find("FPP_Player/Hand").transform;
        cam = GameObject.Find("FPP_Player/Main Camera").GetComponent<Camera>();
    }


    //weapon status
    public virtual void WeaponStatus(Transform status) // aiming or normal
    {
     
        this.transform.SetParent(status);
        this.gameObject.transform.position = status.position;
        this.gameObject.transform.rotation = status.rotation;
     
    }

    public virtual void ReloadProjectile(float reloadTime, ref bool isReloading)
    {
        reloadRunTime += Time.deltaTime;       
        if (reloadRunTime >= reloadTime)
        {
            isReloading = false;
            reloadRunTime = 0;           
        } 
    }


}
