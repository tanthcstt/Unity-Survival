using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class UIController : MonoBehaviour
{
    
    public GameObject inventoryUI;
    public GameObject chestUI;
    public GameObject repaireShipUI;
    public GameObject craftingUI;
    public GameObject interactionNotification;
    public GameObject itemUI;
    public GameObject tradingUI;
    public GameObject pauseUI;

    
    public Camera cam;
    public Transform chestContent;

    public KeyCode inventoryKey = KeyCode.B;
    public KeyCode interactionKey = KeyCode.F;
    public KeyCode craftingKey = KeyCode.I;
    public KeyCode pauseKey = KeyCode.Escape;
    public LayerMask interactiveLayer;

    public bool isUIOn;
    private void Start()
    {
        inventoryUI.SetActive(false);
        chestUI.SetActive(false);
        repaireShipUI.SetActive(false);
        craftingUI.SetActive(false);
        pauseUI.SetActive(false);   

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
     
        
    }
    private void Update()
    {
        CursorToggle();

        if (Input.GetKeyDown(pauseKey)) ToggleByKey(pauseUI);
        if (pauseUI.activeSelf) return;
        
        if (Input.GetKeyDown(inventoryKey))
        {
            ToggleByKey(inventoryUI);
        } else if (Input.GetKeyDown(craftingKey)) {
            ToggleByKey(craftingUI);
        }  

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, 5f, interactiveLayer))
        {

            if (hit.transform.CompareTag("Trader")) 
            {
                if (Input.GetKeyDown(interactionKey)) ToggleByKey(tradingUI);
            } 
        } 
        

      
    }

    public void ToggleByKey(GameObject UI)
    {        
        UI.SetActive(!UI.activeSelf);
        if (UI.activeSelf)
        {
            isUIOn = true;
        }
        else isUIOn = false;
    }

    public void ToggleByRaycast(int layerIndex)
    {
        if (layerIndex == LayerMask.NameToLayer("InteractiveObject"))
        {

            //open chest UI
            chestUI.SetActive(!chestUI.activeSelf);
            if (chestUI.activeSelf)
            {
                isUIOn = true;
            }
            else isUIOn = false;


        }
        else if (layerIndex == LayerMask.NameToLayer("SpaceShip"))
        {
            repaireShipUI.SetActive(!repaireShipUI.activeSelf);

        }

    }

    public void Notification()
    {
        interactionNotification.SetActive(true);
    }
    public void HideNotification()
    {
        if (interactionNotification.activeSelf)
        {
            interactionNotification.SetActive(false);
        }
    }

    public void InstantiateItemUI()
    {
        
        InventoryManager.Instance.InitializeItemUI(ChestManager.Instance.chestList, chestContent, 0, 31, itemUI);
    }

    public void CursorToggle()
    {
        //cursor visible and unlock when at least 1 ui active
        if (isUIOn)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
   
}
