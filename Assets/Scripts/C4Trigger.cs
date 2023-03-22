using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4Trigger : MonoBehaviour
{
    protected bool isPlanting;
    private Camera cam;
    public LayerMask playerLayer;
    public LayerMask itemsLayer;
    private GameObject playerHand;
    public List<GeneralItemData> c4GeneralData = new List<GeneralItemData>();
    private GameObject c4;
    private string[] ignoreLayer = { "Player", "Items" };

    private void Awake()
    {
        cam = GameObject.Find("FPP_Player/Main Camera").GetComponent<Camera>();
        playerHand = GameObject.Find("FPP_Player/Hand");

    }
 
    private void Update()
    {
      
        // hold left mouse to plant C4
        if (transform.parent == playerHand.transform && Input.GetMouseButtonDown(0))
        {
            c4 = Instantiate(gameObject); // instantiate to plant 
            isPlanting = true;
        }
        if (isPlanting && Input.GetMouseButtonUp(0))
        {

            Trigger();
            RemoveItemAfterPlant();
           
        }

        if (!isPlanting) return;
        SetPosition();
    }
    public void SetPosition()
    {
        // plant gameobject;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward,out RaycastHit hit, 10f, ~LayerMask.GetMask(ignoreLayer)))
        {
            c4.transform.position = hit.point;
        }
      

    }
    public void RemoveItemAfterPlant()
    {
        
       
        // remove data from inventory
        c4GeneralData.Clear();
        c4GeneralData.Add(gameObject.GetComponent<GeneralItemData>());
        c4GeneralData[0].itemCount = 1;
        ItemFilter.Instance.IsEnoughItems(c4GeneralData);    
       
       

    }
    public void Trigger()
    {
        isPlanting = false;       
        c4.layer = LayerMask.NameToLayer("Default");
        c4.GetComponent<C4Explode>().isTrigger = true;
    }
}
