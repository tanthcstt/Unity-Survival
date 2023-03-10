using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class CraftingController : MonoBehaviour
{
    public CraftingUI craftingUI;
    public ItemFilter itemFilter;
    public SetCraftedObjPos setCraftedObjPos;
    public ItemInteractions itemInteractions;
    public GameObject player;

    private GameObject newItem;
   

    public int craftingItemID;
    public int materialsID;
    public int materialsAmount;
    public List<GeneralItemData> neddedItems = new List<GeneralItemData>();
    private GeneralItemData materialsData;

    



    public void AddNeededItemToList(int formulaIndex)
    {
        
        //formulaIndex = index of crating item in dictionary
        //get ID of item that is going to be crafted
        //get ID of materials and amount
        // use available generalItemData add to new list
        // adjust amount
        // add to nedded item list
        neddedItems.Clear();
        craftingItemID = CraftingFormula.Instance.craftingFormula.ElementAt(formulaIndex).Key;
        for (int i = 0; i < CraftingFormula.Instance.craftingFormula.ElementAt(formulaIndex).Value.Count; i++)
        {
            materialsID = CraftingFormula.Instance.craftingFormula.ElementAt(formulaIndex).Value.ElementAt(i).Key;
            materialsAmount = CraftingFormula.Instance.craftingFormula.ElementAt(formulaIndex).Value.ElementAt(i).Value;
            materialsData = FindMaterials(materialsID);
            materialsData.itemCount = materialsAmount;
            neddedItems.Add(materialsData);
        }
        Craft();
      
    }

    public GeneralItemData FindMaterials(int materialsID)
    {
        for (int i = 0; i < CraftingFormula.Instance.allCraftingMaterials.Count; i++)
        {
            if (CraftingFormula.Instance.allCraftingMaterials[i].item.itemID == materialsID)
            {
                return CraftingFormula.Instance.allCraftingMaterials[i];
            }
        }
        return null;
    }

    public bool IsEnoughItem()
    {
        if (itemFilter.IsEnoughItems(neddedItems))
        {
           return true;
        } 
        return false;
    }
    public void Craft()
    {
        if (IsEnoughItem())
        {
           
          
            if (GetEnvObject(craftingItemID) != null) // ==> env obj
            {
                GameObject craftedObj = Instantiate(GetEnvObject(craftingItemID));
              
                setCraftedObjPos.SetCraftedObjPosCtrl(craftedObj);
                craftingUI.HideCraftingUI();
            } else if (GetItemObject(craftingItemID) != null) // ==> item
            {
                newItem = Instantiate(GetItemObject(craftingItemID),player.transform.position + (transform.forward * 2f), player.transform.rotation );
                newItem.GetComponent<Rigidbody>().isKinematic = false;
                GeneralItemData itemData = newItem.transform.GetComponent<GeneralItemData>();

                itemInteractions.PickUp(itemData);
                newItem.SetActive(false);

            }

        }
        else
        {
            Debug.Log("you dont have enough materials");
        }
    }

    public GameObject GetEnvObject(int craftingItemID) // env obj type
    {
        for (int i = 0; i < CraftingFormula.Instance.allCraftedItems.Count; i++)
        {
            BreakableObjData envObj = CraftingFormula.Instance.allCraftedItems[i].GetComponent<BreakableObjData>();            
            if (envObj != null && envObj.generalData.ObjID == craftingItemID)
            {
                return CraftingFormula.Instance.allCraftedItems[i];
            }
        }

        return null;
    }

    public GameObject GetItemObject(int craftingItemID)// item type
    {
        for (int i = 0; i < CraftingFormula.Instance.allCraftedItems.Count; i++)
        {
            GeneralItemData itemObj = CraftingFormula.Instance.allCraftedItems[i].GetComponent<GeneralItemData>();
            if (itemObj != null && itemObj.item.itemID == craftingItemID)
            {
                return CraftingFormula.Instance.allCraftedItems[i];
            }
        }

        return null;
    } 
   
 
}
