using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public List<GeneralItemData> inventory = new List<GeneralItemData>();    
    public List<float> playerPosition = new List<float>();
    public void GetPlayerPos(GameObject player)
    {
       
        playerPosition.Clear(); 
        playerPosition.Add(player.transform.position.x);
        playerPosition.Add(player.transform.position.y);
        playerPosition.Add(player.transform.position.z);
    }
    public void GetPlayerInventory()
    {
        inventory.Clear();
        inventory.AddRange(InventoryManager.Instance.hotbarList);
    }
}
