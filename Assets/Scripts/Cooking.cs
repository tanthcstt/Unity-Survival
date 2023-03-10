using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cooking : MonoBehaviour
{
    protected int childIndex;
    protected Transform hand;
    public ItemInteractions itemInteractions;
    public List<GeneralItemData> cookingList = new List<GeneralItemData>();
    protected GameObject cookedItem;
    private void Awake()
    {
        hand = GameObject.Find("FPP_Player/Hand").transform;
    }
    public void Cook()
    {
        SetCookingList();
        ChangeFood();
        AddToInventory();
    }
    protected void SetCookingList()
    {
        for (int i = 0; i < hand.childCount; i++)
        {
            Debug.Log(hand.GetChild(i).gameObject.activeSelf);
            if (!hand.GetChild(i).gameObject.activeSelf) break;
            if (!hand.GetChild(i).GetComponent<GeneralItemData>().item.isCookable) break;  
            childIndex = i; 
            GeneralItemData cookingItemData = hand.GetChild(i).GetComponent<GeneralItemData>();
            cookingList.Add(cookingItemData);
        }
    }   
    protected void ChangeFood()
    {
        cookingList[0].itemCount = 1;
        if (ItemFilter.Instance.IsEnoughItems(cookingList))
        {
        
            DestroyImmediate(hand.GetChild(childIndex).gameObject);
            cookedItem = Instantiate(cookingList[0].item.cookedItem, hand.transform.position, hand.transform.rotation);
            cookedItem.transform.SetParent(hand.transform);
            cookedItem.GetComponent<Rigidbody>().isKinematic = true;
            cookedItem.transform.SetSiblingIndex(childIndex);

        }
      
    }
    protected void AddToInventory()
    {
        itemInteractions.PickUp(cookedItem.GetComponent<GeneralItemData>());
    }
}
