using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradingData : MonoBehaviour
{
    public List<GeneralItemData> currentTradinngItem = new List<GeneralItemData>(); 
    public List<GeneralItemData> payment = new List<GeneralItemData>();
    public ItemInteractions itemInteractions;
    public GameObject moneyPrefabs;

    public void SetCurrentTradingItem(GeneralItemData itemData)
    {
        currentTradinngItem.Clear();
        currentTradinngItem.Add(itemData);
       
    }

    public void AddItem()
    {
        GameObject boughtItem = Instantiate(currentTradinngItem[0].item.obj);
        itemInteractions.PickUp(boughtItem.GetComponent<GeneralItemData>());
    }
    public void AddMoney()
    {
        GameObject money = Instantiate(moneyPrefabs);
        money.GetComponent<GeneralItemData>().itemCount = currentTradinngItem[0].item.price;
        itemInteractions.PickUp(money.GetComponent<GeneralItemData>()); 
    }
    public bool IsEnoughMoney()
    {
        payment[0].itemCount = currentTradinngItem[0].item.price;
        if (ItemFilter.Instance.IsEnoughItems(payment)) return true;
        Debug.Log("you dont have enough money");
        return false;
    }

    public bool IsEnoughItem()
    {
        int sellAmount = currentTradinngItem[0].itemCount;    
        if (ItemFilter.Instance.IsEnoughItems(currentTradinngItem))// after use itemfilter, amount of item will be reset to 0
        {
            currentTradinngItem[0].itemCount = sellAmount; // redefine amount of item in trade list
            return true;
        }
        Debug.Log("you dont have enough item");
        return false;
    }

}
