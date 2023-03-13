using Mono.Cecil.Cil;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;


public class FPP_Interaction : MonoBehaviour
{
    
    public int maxHealth = 10;
    public float maxStamina = 100f;
    private int currentHealth;
    private float currentStamina;
    private int selectedItemID;
   
    public HealthBar healthBar;
    public StaminaBar staminaBar;
    public Camera cam;
    
    public LayerMask itemLayer;
    public LayerMask interactiveObject;

    public KeyCode interactionKey = KeyCode.F;
    

    public GameObject inventory;
    public GameObject pickUpNotification;
    public GameObject playerHand;
    public GameObject itemInHand;
    public GameObject testObj;

    public ItemInteractions ItemInteractions;
    public SpaceShipInteractions SpaceShipInteractions;
    public ShipUI shipUI;
    public BreakObj breakObj;
    public UIController UIController;
    public Cooking cooking;

    Rigidbody itemRb;
  
   
    private String[] interactableLayer = { "Items", "InteractiveObject", "SpaceShip", "BreakableObject" };
    private KeyCode[] selectedItem = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8};

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        currentStamina = maxStamina;
        staminaBar.SetMaxStamina(maxStamina);

        selectedItemID = -1;
        itemInHand = null;


    }

    // Update is called once per frame
    void Update()
    {
        // put item in hotbar list on player hand
        SelectItem();
        HandReset();
        // raycast to breakable item
        BreakObject();


        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage();
        }
       
       if(Input.GetKey(KeyCode.LeftShift))
       {
            StaminaConsuming();
         
       } else if (!(Input.GetKey(KeyCode.LeftShift)) && currentStamina <= maxStamina)
       {
            RefillStamina();
       } 

      
       
       
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 5f, LayerMask.GetMask(interactableLayer)))
        {
            UIController.Notification();
            if (Input.GetKeyDown(interactionKey))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("InteractiveObject") && hit.transform.gameObject.CompareTag("Chest"))
                {
                    // get data of chest that raycast hit
                    ChestData chestData = hit.transform.gameObject.GetComponent<ChestData>();
                    ChestManager.Instance.ChestList(chestData.chestItemList);

                    
                    UIController.InstantiateItemUI();

                    // toggle UI when raycast hit a specific layer
                    UIController.ToggleByRaycast(hit.transform.gameObject.layer);
                } else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Items"))
                {
                    PickUpItem(hit);
                } else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("SpaceShip"))
                {
                    if (!SpaceShipInteractions.isBroken)
                    {
                        SpaceShipInteractions.GetIn();
                    }
                } else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("InteractiveObject") && hit.collider.CompareTag("CampFire"))
                {
                    if (itemInHand == null) return;
                    if (!itemInHand.GetComponent<GeneralItemData>().item.isCookable) return;
                    cooking.Cook();
                  
                } 


             
              
              
            }

        } else
        {
            UIController.HideNotification();
        }

    }
    void TakeDamage()
    {
        currentHealth -= 5;
        healthBar.SetHealth(currentHealth);
    }

    void StaminaConsuming()
    {
        currentStamina -= .1f;
        staminaBar.SetStamina(currentStamina);
    }

    void RefillStamina()
    {
        currentStamina += 1f;
        staminaBar.SetStamina(currentStamina);
    }

    void PickUpItem(RaycastHit hit)
    {         
        
            // destroy game object and call function  to add item to ItemManager
        var itemData = hit.transform.gameObject.GetComponent<GeneralItemData>();
        ItemInteractions.PickUp(itemData);
        hit.collider.gameObject.SetActive(false);
    }
    
    void SelectItem()
    {
        for (int i = 0; i < selectedItem.Length; i++)
        {
            if (Input.GetKeyDown(selectedItem[i]))
            {
                // set selected item id
                if (InventoryManager.Instance.hotbarList[i] == null)
                {
                    selectedItemID = -1;
                } else
                {
                    selectedItemID = InventoryManager.Instance.hotbarList[i].item.itemID;
                }

                //setActive and put item on hand
                for(int j = 0; j < InventoryManager.Instance.hotbarList.Count; j++)
                {
                    if (InventoryManager.Instance.hotbarList[j] != null)
                    {
                        if (j == i)
                        {
                            InventoryManager.Instance.hotbarList[j].gameObject.SetActive(true);
                            InventoryManager.Instance.hotbarList[j].gameObject.transform.position = playerHand.transform.position;
                            InventoryManager.Instance.hotbarList[j].gameObject.transform.rotation = playerHand.transform.rotation;
                            InventoryManager.Instance.hotbarList[j].gameObject.transform.SetParent(playerHand.transform);
                            itemRb = InventoryManager.Instance.hotbarList[i].gameObject.GetComponent<Rigidbody>();
                            if (itemRb != null) itemRb.isKinematic = true;
                                // consumable obj
                                if (InventoryManager.Instance.hotbarList[j].gameObject.GetComponent<GeneralItemData>().item.isConsumable)
                            {
                                PlayerConsuming playerConsuming = gameObject.GetComponent<PlayerConsuming>();
                                playerConsuming.isConsumable = true;
                                playerConsuming.GetConsumeObjectData(InventoryManager.Instance.hotbarList[j].gameObject.GetComponent<GeneralItemData>());

                            }
                            itemInHand = InventoryManager.Instance.hotbarList[j].gameObject;

                            if (InventoryManager.Instance.hotbarList[j].gameObject.GetComponent<GeneralItemData>() &&
                                InventoryManager.Instance.hotbarList[j].gameObject.GetComponent<GeneralItemData>().item.isDoDamage)
                            {
                                EnableDoDamageItem(InventoryManager.Instance.hotbarList[j]);
                            }

                                
                         
                        }
                        else
                        {
                            if (InventoryManager.Instance.hotbarList[j].gameObject.GetComponent<GeneralItemData>() &&
                              InventoryManager.Instance.hotbarList[j].gameObject.GetComponent<GeneralItemData>().item.isDoDamage)
                            {
                                
                                DisableDoDamageItem(InventoryManager.Instance.hotbarList[j]);
                               
                            }
                      

                            InventoryManager.Instance.hotbarList[j].gameObject.SetActive(false);

                        }
                    } 
                }
                // 
               
            }
        }

    }

    void BreakObject()
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 10f)
            && hit.transform.gameObject.layer == LayerMask.NameToLayer("BreakableObject")
            && Input.GetMouseButtonDown(0))
        {
           
            BreakableObjData breakableObjData = hit.transform.parent.GetComponent<BreakableObjData>();
            breakObj.Break(breakableObjData, selectedItemID);
        }
    }

    // need to optimize
    void EnableDoDamageItem(GeneralItemData itemData) // equiped if item can do damage;
    {
        Shooting shootingWeapon = itemData.gameObject.GetComponent<Shooting>();
        MeleeWeapom meleeWeapom = itemData.gameObject.GetComponent<MeleeWeapom>();
        
        if (shootingWeapon == null && meleeWeapom == null) return;
        if (shootingWeapon) shootingWeapon.isEquiped = true;
        if (meleeWeapom) meleeWeapom.isEquiped = true;
    }
    void DisableDoDamageItem(GeneralItemData itemData) // equiped if item can do damage;
    {
        Shooting shootingWeapon = itemData.gameObject.GetComponent<Shooting>();
        MeleeWeapom meleeWeapom = itemData.gameObject.GetComponent<MeleeWeapom>();
        if (shootingWeapon == null && meleeWeapom == null) return;
        if (shootingWeapon) shootingWeapon.isEquiped = false;
        if (meleeWeapom) meleeWeapom.isEquiped = false;

    }

    private void HandReset()
    {
        bool isInHand = false;
        if (itemInHand == null) return;
        for (int i = 0; i < InventoryManager.Instance.hotbarList.Count; i++)
        {
            
            if (InventoryManager.Instance.hotbarList[i] != null && itemInHand.GetComponent<GeneralItemData>().item.itemID == InventoryManager.Instance.hotbarList[i].item.itemID)
            {
                isInHand = true;    
            }
        }
        if (!isInHand)
        {
            Destroy(itemInHand);
            itemInHand = null;
        }

    }
}
