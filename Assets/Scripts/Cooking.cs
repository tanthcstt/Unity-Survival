using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cooking : MonoBehaviour
{
    protected int childIndex;
    protected Transform hand;
    public ItemInteractions itemInteractions;
    public GameObject cookedItem;
    public List<GeneralItemData> rawFoodList = new List<GeneralItemData>();
    public GameObject rawFoodPrefabs;
   
  
    private void Awake()
    {
        hand = GameObject.Find("FPP_Player/Hand").transform;
        SetRawFoodList();
      
    }
    public void Cook()
    {
        rawFoodList[0].itemCount = 1;
        if (ItemFilter.Instance.IsEnoughItems(rawFoodList))
        {
            ChangeFood();
            AddToInventory();   
        }
       
    }

    protected void ChangeFood()
    {

        cookedItem = Instantiate(rawFoodList[0].item.cookedItem, hand.transform.position, hand.transform.rotation);
        cookedItem.transform.SetParent(hand.transform);
        cookedItem.GetComponent<Rigidbody>().isKinematic = true;
      


    }
    protected void AddToInventory()
    {
        itemInteractions.PickUp(cookedItem.GetComponent<GeneralItemData>());
    }
    protected void SetRawFoodList()
    {
        GameObject rawFood = Instantiate(rawFoodPrefabs, new Vector3(0,0,0), Quaternion.identity);
        rawFoodList.Add(rawFood.GetComponent<GeneralItemData>());
      
    }
}
