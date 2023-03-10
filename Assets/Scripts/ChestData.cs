using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestData : MonoBehaviour
{
    public List<GeneralItemData> chestItemList = new List<GeneralItemData>();
    public Transform chestManager;
    public int chestID;

    private void Start()
    {
        chestID = transform.GetSiblingIndex();
        
        for (int i = 0; i < 31; i++)
        {
            chestItemList.Add(null);
        }
    }

}
