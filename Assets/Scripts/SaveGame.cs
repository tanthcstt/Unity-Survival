using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
     
    public void Save()
    {
        GetPlayerData();
        WiteFile();
        Debug.Log("saved");
    }
    public void GetPlayerData()
    {
        GameDataManager.instance.GetData();
    }
    public void WiteFile()
    {
        string jsonString = JsonUtility.ToJson(GameDataManager.instance.playerData);
        File.WriteAllText(GameDataManager.instance.path, jsonString);
        Debug.Log("saved" + "  " + jsonString);
        File.Exists(GameDataManager.instance.path);
    }
}
