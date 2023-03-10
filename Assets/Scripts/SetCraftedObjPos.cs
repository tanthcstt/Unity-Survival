using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SetCraftedObjPos : MonoBehaviour
{
    public bool isCrafted;
    public GameObject cam;
    public CraftingController craftingController;
    public LayerMask envLayer;
    public Transform groundCheck;
    public GameObject player;
    private GameObject craftedObj;



    private void Update()
    {
        if (isCrafted)
        {
           
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 10f, envLayer))
            {
                if (hit.collider != null)
                {
                    craftedObj.transform.position = hit.point;
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            isCrafted = false;
        }
    }

    public void SetCraftedObjPosCtrl(GameObject craftedObj)
    {
        isCrafted = true;
        this.craftedObj = craftedObj;       
    }


        
}
