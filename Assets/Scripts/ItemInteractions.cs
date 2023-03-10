using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ItemInteractions : MonoBehaviour
{
    public GameObject player;
    public void PickUp(GeneralItemData itemData)
    {
       
        InventoryManager.Instance.AddItemController(itemData);
        InventoryManager.Instance.ItemUIController(itemData);

    }

    public void Drop(GeneralItemData itemData, GameObject itemUI)
    {
        
        // remove data from list
        
        //destroy UI 
        // instantiate object 
        if (itemData.item.stackable)
        {
            // instantiate
            if (itemData.itemCount > 1)
            {
                itemData.itemCount--;
                InventoryManager.Instance.ItemUIController(itemData);
            } else
            {
                InventoryManager.Instance.invetoryList.Remove(itemData);
                Destroy(itemUI);

            }
            Instantiate(itemData.item.obj, player.transform.position + (player.transform.forward * 2),player.transform.rotation);
        } else
        {
            //SetActive && position
            InventoryManager.Instance.invetoryList.Remove(itemData);
            itemData.gameObject.SetActive(true);
            itemData.gameObject.transform.position = player.transform.position +(player.transform.forward * 2);
            Destroy(itemUI);
        }
    }     





}
 

   

