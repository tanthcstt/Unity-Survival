using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FixShip : MonoBehaviour
{
    public int requiredItem_1_Amount;
    public int requiredItem_2_Amount;
    public List<GeneralItemData> playerItems;
    public Transform itemParent;
    public Button repairButton;
    public GameObject itemUI;

    public ItemFilter itemFilter;
    public SpaceShipInteractions spaceShipInteractions;

    public List<GeneralItemData> requiredItems = new List<GeneralItemData>();
    private void Awake()
    {
        RequiredItemAmount();
    }
    private void Start()
    {
        // initialize UI of required items
        InventoryManager.Instance.InitializeItemUI(requiredItems, itemParent, 0, 2, itemUI);

        // get button component
        repairButton.onClick.AddListener(RepairSpaceShip);
       
    }
    public void RequiredItemAmount()
    {
        requiredItems[0].itemCount = 1;
        requiredItems[1].itemCount = 3;

    }

    public void RepairSpaceShip()
    {
        
        if (itemFilter.IsEnoughItems(requiredItems))
        {
            Debug.Log("SpaceShip Repaired");
            spaceShipInteractions.isBroken = false;
            
            
        }
    }
   
}
