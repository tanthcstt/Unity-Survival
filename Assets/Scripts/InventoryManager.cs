using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public List<GeneralItemData> hotbarList = new List<GeneralItemData>();
    public List<GeneralItemData> invetoryList = new List<GeneralItemData>();
    
    public ItemInteractions interaction;
    public Transform inventoryContent; 
    public Transform chestContent;
   
    public Transform hotBarContent;
    public GameObject inventoryItem;
    public GameObject hotbarItem;
    public GameObject player;
    

    private void Awake()
    {
        Instance = this;
        AddNullItem(hotbarList, 8);
        AddNullItem(invetoryList, 31);
    }
  

    
    // Add item controller
    public void AddItemController(GeneralItemData itemData)
    {
      
        if (ItemCount(hotbarList) < 8 || (ItemCount(hotbarList) >= 8 && !StackableItemFull(hotbarList, itemData)))
        {
            // add to hot bar
          
            AddToList(itemData, hotbarList);
        }
        else
        {
          

            // add to inventory
            AddToList(itemData, invetoryList);
        }

    }
    public int ItemCount(List<GeneralItemData> itemData)
    {
        int count = 0;
        for (int i = 0; i < itemData.Count; i++)
        {
            if (itemData[i] != null) count++;
        }
        return count;
    }
    // add to list
    public void AddToList(GeneralItemData itemData, List<GeneralItemData> itemList)
    {
       if (itemData.item.stackable)
       {
            // stackable
            // check for available stackable item
            if (StackableItemFull(itemList, itemData)) // full ==>  add new item
            {
                // get slot
                var emptySlot = EmptySlot(itemList);
                if (emptySlot >= 0)
                {
                    if (itemData.itemCount == 0) // if item has amount > 1, dont change
                    {
                        itemData.itemCount++;                      
                    }
                    itemList.Insert(emptySlot, itemData);
                }

            } else // increase available stackable item
            {
                // get index of available item ==> increase item amount 
                itemList[GetNotFullStackSlot(itemList, itemData)].itemCount++;

            }
       } else
       {
            // unstackable
            // get slot
            var slot = EmptySlot(itemList);
            if (slot >= 0)
            {
                itemList.Insert(slot, itemData);
            }
       }
    }

    // get slot for unstackable item
    public int GetUnstackableSlot(List<GeneralItemData> itemList)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == null)
            {
                return i;
            }
        }
        Debug.Log("GetUnstackableSlot");
        return -1;
     
    }

    public void AddNullItem(List<GeneralItemData> itemList, int maxSlot)
    {
        if (itemList.Count == 0)
        {
            for (int i = 0; i < maxSlot; i++)
            {
                itemList.Add(null);
            }
        }
    }

    public bool StackableItemFull(List<GeneralItemData> itemList, GeneralItemData itemData)
    {

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] != null)
            {
                if (itemData.item.stackable && itemData.item == itemList[i].item && itemList[i].itemCount == itemList[i].item.maxAmount)
                {
                    continue;
                } else if (itemData.item.stackable && itemData.item == itemList[i].item && itemList[i].itemCount < itemList[i].item.maxAmount)
                {
                    return false;
                }
            } 
        }
        return true;     
    }
   

    public int GetNotFullStackSlot(List<GeneralItemData> itemList, GeneralItemData itemData)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] != null && itemList[i].item == itemData.item && itemList[i].itemCount < itemData.item.maxAmount)
            {
                return i;
            }
        }
        Debug.Log("full stack all ==> add new stack");
        return -1;
    }
    // clear data in empty slot
    public int EmptySlot(List<GeneralItemData> itemList)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i] == null)
            {
                itemList.RemoveAt(i);
                return i;
            }
        }
        Debug.Log("Empty slot function return -1"); 
        return -1;
    } 


    public void ItemUIController(GeneralItemData itemData)
    {

       
        InitializeItemUI(hotbarList, hotBarContent, 0, hotbarList.Count, hotbarItem);
        if (invetoryList.Count > 0)
        {
            

            InitializeItemUI(invetoryList, inventoryContent, 0, invetoryList.Count, inventoryItem);
        }

    }

    public void InitializeItemUI(List<GeneralItemData> itemList, Transform itemParent, int startNum, int endNum, GameObject itemUIPrefab)
    {
        // clear item UI
        foreach (Transform item2d in itemParent)
        {
            
                for (int i = 0; i < item2d.childCount; i++)
                {
                    Destroy(item2d.transform.GetChild(i).gameObject);
                }
            
           
        }

        //initialize item UI
        for (int i = startNum; i < endNum; i++)
        {
            if (itemList[i] == null)
            {
                continue;
            }

            GameObject itemUI = Instantiate(itemUIPrefab, itemParent.GetChild(i));
            var itemName = itemUI.transform.Find("ItemName").GetComponent<Text>();
            var itemSprite = itemUI.transform.Find("ItemSprite").GetComponent<Image>();
            var itemCount = itemUI.transform.Find("Count").GetComponent<Text>();

            // add remnove btn Listener 
            if (itemUIPrefab == inventoryItem)
            {
                AddDropBtnListener(itemList[i], itemUI);
            }

            // set data for itemUI
            if (!itemList[i].item.stackable)
            {
                itemCount.text = " ";
            }
            else
            {
                itemCount.text = itemList[i].itemCount.ToString();
            }
            itemName.text = itemList[i].item.itemName;
            itemSprite.sprite = itemList[i].item.itemSprite;
        }
    }

    public void AddDropBtnListener(GeneralItemData itemData, GameObject itemUI)
    {
        var dropBtn = itemUI.transform.Find("RemoveItem").GetComponent<Button>();
        dropBtn.onClick.AddListener(delegate
        {
            interaction.Drop(itemData, itemUI);
        });
    }

    

    public void ChangeStorageListController(Transform beforeDragSlot, Transform afterDragSlot)
    {

        SetItemPosition(beforeDragSlot, afterDragSlot, GetItemList(beforeDragSlot.parent), GetItemList(afterDragSlot.parent));
     
    }

    public List<GeneralItemData> GetItemList(Transform UIMenu) // get list corresponding to UIMenu
    {
        if (UIMenu == hotBarContent)
        {
            return hotbarList;
        } else if (UIMenu == inventoryContent)
        {
            return invetoryList;
        } else if (UIMenu == chestContent)
        {
          
            return ChestManager.Instance.chestList;
        }
        Debug.Log("GetItemList");
        return null;
    }

   

    public void SetItemPosition(Transform beforeDragSlot, Transform afterDragSlot, List<GeneralItemData> currentList, List<GeneralItemData> newList)
    {
        var selected = currentList.Where(item => item == currentList[beforeDragSlot.GetSiblingIndex()]).ToList();        
        newList.AddRange(selected);
        currentList[beforeDragSlot.GetSiblingIndex()] = null;

        GeneralItemData temp = newList[newList.Count - 1];
        newList[afterDragSlot.GetSiblingIndex()] = temp;
        newList.RemoveAt(newList.Count - 1);
    }


}
