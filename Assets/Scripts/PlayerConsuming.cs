using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConsuming : MonoBehaviour
{
    public bool isConsumable;
    public List<GeneralItemData> consumeList = new List<GeneralItemData>();
    public PlayerHealth playerHealth;
    [Header("item increasing amount")]
    public float food = 3f;
    public float drink = 2f;
    public float medKit = 10f;
    private void Awake()
    {
        consumeList.Add(null);
    }
    private void Update()
    {
        
        if (isConsumable && Input.GetMouseButtonDown(1))
        {
            Consume();
            IncreasePlayerParameter();


        }
    }
    public void Consume()
    {
       // consume item : food/ drink, medicine.... by item fillter function;
       // increase player parameter
        if (!ItemFilter.Instance.IsEnoughItems(consumeList))
        {
            isConsumable = false;
        }
       

    }
    public void GetConsumeObjectData(GeneralItemData itemData)
    {
        consumeList[0] = itemData;  
    }
    public void IncreasePlayerParameter()
    {
        GeneralItemData ItemToConsum = consumeList[0];
        if (ItemToConsum.item.itemID == 12) // cooked food ==> increase Food;
        {
            playerHealth.IncreasePlayerParameters(ref playerHealth.hungryTime, food, playerHealth.maxHungryTime);
        } else if (ItemToConsum.item.itemID == 13) {       // medkit increase health
            playerHealth.IncreasePlayerParameters(ref playerHealth.health, - medKit, playerHealth.maxHealth); 
        }
            // INSERT DRINK OR ANOTHER ITEM CAN INCREASING PLAYER PARAMETER HERE 
        
    }

}
