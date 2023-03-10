using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BreakObj : MonoBehaviour
{

    public Transform enviroment;
    public void Break(BreakableObjData objData, int toolToBreakID)
    {
        if (toolToBreakID == objData.generalData.ObjID && objData.health == 0)
        {
            Instantiate(objData.generalData.DropObj, objData.transform.position + (transform.up * 2f), objData.transform.rotation) ;
            if (objData.generalData.isHaveBrokenObj)
            {
                Instantiate(objData.generalData.BrokenObj, objData.transform.position, objData.transform.rotation) ;
            }
           
            objData.gameObject.SetActive(false);    
        } else if (toolToBreakID == objData.generalData.ObjID)
        {
            objData.health -= 1;
        } else
        {
            Debug.Log("You need an Axe");
        }
    }
}
