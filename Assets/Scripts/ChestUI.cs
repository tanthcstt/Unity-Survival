using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestUI : MonoBehaviour
{
    public GameObject slotPrefab;
    public Transform chestContent;
    public void LoadItemUI(List<GeneralItemData> itemList)
    {
        InventoryManager.Instance.InitializeItemUI(itemList, chestContent, 0, itemList.Count, slotPrefab);
    }
}
