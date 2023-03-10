using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// create scriptable object in Asset/Item
[CreateAssetMenu(fileName = "Scriptabe_Obj", menuName = "Item/Create Item")]
public class ItemScriptableObject : ScriptableObject
{
    //general data
    public GameObject obj;  
    public Sprite itemSprite;
    public int itemID;
    public string itemName;
    public int maxAmount = 3;
    public int itemAmount = 0;
    public bool stackable;

    //weapon
    public int itemDamage;
    public bool isShootable;
    public bool isDoDamage;
    public List<GameObject> arrows_bullets;

    //crafting
    public bool isMaterial;

    //food - drink - med
    public bool isConsumable;
    public bool isCookable;
    public GameObject cookedItem;
    // trade
    public int price;
  
    
}