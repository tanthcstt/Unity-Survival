using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;

    public PlayerData playerData;
    public SaveGame saveGame;
    public LoadGame loadGame;
    [SerializeField] GameObject player;

    public string path;

    private void Awake()
    {
        instance = this;    
        playerData = new PlayerData();  
        path = Application.persistentDataPath + "/fakeGameData.demo";
      
    }
    private void Start()
    {
        loadGame.SetData();
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Z))
        {
            saveGame.Save();
        }   
    }
   
   
    public void GetData()
    {
        playerData.GetPlayerPos(player);
        playerData.GetPlayerInventory();
    }
   
}



