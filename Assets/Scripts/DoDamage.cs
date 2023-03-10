using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    
    
    public bool isEquiped;
    private bool isStartShoot;
   
    public List<GeneralItemData> requiredArrow = new List<GeneralItemData>() ;
    private void Start()
    {
        requiredArrow.Add(gameObject.GetComponent<GeneralItemData>().item.arrows_bullets[0].GetComponent<GeneralItemData>());
    }

    private void Update()
    {
        if (isEquiped)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (gameObject.GetComponent<GeneralItemData>().item.isShootable)
                {
                    SetArrowType();                   
                    isStartShoot = true;    
                }            

            }
            if (isStartShoot && Input.GetMouseButtonUp(0))
            {
                // if is enough arr ==> shoot
                if (isEnoughArrow(requiredArrow))
                {
                    DoDamageByShoot();
                } else
                {
                    Debug.Log("You don't have any arrows");
                }               

              
                isStartShoot = false;
            }

        }
    }
   

    private void DoDamageByShoot()
    {
        //  choose arrow  / bullet
        GameObject arrows_bulletsPrefab = gameObject.GetComponent<GeneralItemData>().item.arrows_bullets[0];
        
        ArrowTrajectory.Instance.ArrowFly(arrows_bulletsPrefab);
       
        // add force for arrow to fly
    }

    private void SetArrowType() //  check amount of bullets/arrows in inventory
    {
        GeneralItemData arrowData = gameObject.GetComponent<GeneralItemData>().item.arrows_bullets[0].GetComponent<GeneralItemData>();
        arrowData.itemCount = 1;
        requiredArrow.Add(arrowData);
        
       
    }
    private bool isEnoughArrow(List<GeneralItemData> requiredArrow) 
    {
        if (ItemFilter.Instance.IsEnoughItems(requiredArrow)) 
        {
            return true;
        }
        return false;
    }
    

}
