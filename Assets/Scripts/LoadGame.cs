using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public GameObject player;
    private PlayerData playerData;
    public ItemInteractions itemInteractions;

   
    public void SetData()
    {
        playerData = GameDataManager.instance.playerData;
        ReadJson();
        SetPlayerPos();
        SetPlayerInventory();

    }
    public void ReadJson()
    {
        if (File.Exists(GameDataManager.instance.path))
        {
            string JsonContent = File.ReadAllText(GameDataManager.instance.path);

            playerData = JsonUtility.FromJson<PlayerData>(JsonContent);


        }
        File.Exists(GameDataManager.instance.path);
    }
    public void SetPlayerPos()
    {
        Vector3 pos = new Vector3(playerData.playerPosition[0], playerData.playerPosition[1], playerData.playerPosition[2]);
        player.transform.position = pos;
    }
    public void SetPlayerInventory()
    {
        foreach(GeneralItemData item in playerData.inventory)
        {
            if (item != null)
            {
                itemInteractions.PickUp(item);
            }
        }   
    }
}
