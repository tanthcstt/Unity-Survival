using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class ItemFilter : MonoBehaviour
{
    public static ItemFilter Instance;
    
    private bool isEnough;
    private bool isLastItem;
    private int totalStackableItems;
    private int totalOfList_1;
    private int totalOfList_2;
    private int totalUnstackable;

    public Transform hotbarContent;
    public Transform inventoryContent;
    public GameObject itemUI;
    private void Awake()
    {
        Instance = this;
    }

    // if call this function with parameter List<>.itemcount = 0 it will return true;
    // itemcount int require item list will be = 0 if this function return true;
    public bool IsEnoughItems(List<GeneralItemData> requiredItems)
    {
       
        for (int i = 0; i < requiredItems.Count; i++)
        {           
          
            if (requiredItems[i].item.stackable)
            {
                CheckStackableItem(InventoryManager.Instance.hotbarList, InventoryManager.Instance.invetoryList, requiredItems[i]);
                
                
            } else // unstackable
            {
                CheckUnStackableItem(InventoryManager.Instance.hotbarList, InventoryManager.Instance.invetoryList, requiredItems[i]);

            }
            if (!isEnough)
            {
                return false;
            }
        }      
       
        if (isEnough) // update UI
        {
            for (int i = 0; i < requiredItems.Count; i++)
            {
                if (requiredItems[i].item.stackable)
                {
                    DecreaseController(requiredItems[i], InventoryManager.Instance.hotbarList, InventoryManager.Instance.invetoryList);
                } else 
                {
                    // remove unstackable item
                    RemoveUnstackableItem(InventoryManager.Instance.hotbarList, InventoryManager.Instance.invetoryList, requiredItems[i]);
                }
            }
            
            UpdateInventoryUI(InventoryManager.Instance.hotbarList, hotbarContent);
            UpdateInventoryUI(InventoryManager.Instance.invetoryList, inventoryContent);
        }

        return isEnough;
    }

    public void CheckUnStackableItem(List<GeneralItemData> listToCheck_1, List<GeneralItemData> listToCheck_2, GeneralItemData itemToCheck)
    {
        if (IsUnstackableItem(InventoryManager.Instance.hotbarList, InventoryManager.Instance.invetoryList, itemToCheck))
        {
            isEnough = true;
        } else
        {
            isEnough = false;
        }


    }

    public bool IsUnstackableItem(List<GeneralItemData> listToCheck_1, List<GeneralItemData> listToCheck_2, GeneralItemData itemToCheck)
    {
        for (int i = 0; i < listToCheck_1.Count; i++)
        {
            if (listToCheck_1[i] != null && listToCheck_1[i].item.itemID == itemToCheck.item.itemID)
            {
                return true;
            }
        }
        for (int i = 0; i < listToCheck_2.Count; i++)
        {
            if (listToCheck_2[i] != null && listToCheck_2[i].item.itemID == itemToCheck.item.itemID)
            {
                return true;
            }
        }
        return false;
    }
    public void CheckStackableItem(List<GeneralItemData> listToCheck_1, List<GeneralItemData> listToCheck_2, GeneralItemData itemToCheck)
    {
        totalOfList_1 = StackableItemCount(listToCheck_1, itemToCheck);
        totalOfList_2 = StackableItemCount(listToCheck_2, itemToCheck);
        totalStackableItems = totalOfList_1 + totalOfList_2;
      
        if (totalOfList_1 >= itemToCheck.itemCount || totalOfList_2 >= itemToCheck.itemCount)
        {           
            isEnough = true;
       
           
        } else if (totalOfList_1 + totalOfList_2 >= itemToCheck.itemCount)
        {
            isEnough = true;
         
        } else
        {
            isEnough = false;           
        }
       
    }

    public int StackableItemCount(List<GeneralItemData> listToCheck, GeneralItemData itemToCheck)
    {
        totalStackableItems = 0;
        for (int i = 0; i < listToCheck.Count; i++)
        {
            if (listToCheck[i] != null && listToCheck[i].item.itemID == itemToCheck.item.itemID)
            {
                totalStackableItems += listToCheck[i].itemCount;
            }
        }
       
        return totalStackableItems;
    }

    public void UpdateInventoryUI(List<GeneralItemData> listToUpdate, Transform UIParent)
    {
        InventoryManager.Instance.InitializeItemUI(listToUpdate, UIParent, 0, listToUpdate.Count, itemUI);
    }
    public void DecreaseController(GeneralItemData itemToDecrease, List<GeneralItemData> listToDecrease_1, List<GeneralItemData> listToDecrease_2)
    {
        for (int i = 0; itemToDecrease.itemCount > 0; i++)
        {
            if (StackableItemCount(listToDecrease_1, itemToDecrease) > 0)
            {
                DecreaseInList(listToDecrease_1, itemToDecrease);

            } else
            {
                //decrease in list 2
                DecreaseInList(listToDecrease_2, itemToDecrease);
            }
        }
    }
    public void DecreaseInList(List<GeneralItemData> listToDecrease , GeneralItemData itemToDecrease)
    {
      
        for (int i = 0; i < listToDecrease.Count; i++)
        {
            if (itemToDecrease.itemCount == 0)
            {
                break;
            }
            if (listToDecrease[i] != null &&  itemToDecrease.item.itemID == listToDecrease[i].item.itemID)
            {
                if (listToDecrease[i].itemCount > itemToDecrease.itemCount) // available in 1 slot > required
                {
                    listToDecrease[i].itemCount -= itemToDecrease.itemCount;
                    itemToDecrease.itemCount = 0;
                }
                else if (listToDecrease[i] != null) // available in 1 slot <= required
                {
                    itemToDecrease.itemCount -= listToDecrease[i].itemCount;                   
                    listToDecrease[i] = null;
                }
            }
        }


    }
    public void RemoveUnstackableItem(List<GeneralItemData> listToRemove_1, List<GeneralItemData> listToRemove_2, GeneralItemData itemToRemove)
    {
        for (int i = 0; i < listToRemove_1.Count; i++)
        {
            if (listToRemove_1[i] != null && itemToRemove.item.itemID == listToRemove_1[i].item.itemID)
            {
                listToRemove_1[i] = null;
                return;
            }
        }

        for (int i = 0; i < listToRemove_2.Count; i++)
        {
            if (listToRemove_2[i] != null && itemToRemove.item.itemID == listToRemove_2[i].item.itemID)
            {
                listToRemove_2[i] = null;
                return;
            }
        }
    }
}

