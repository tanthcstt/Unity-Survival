using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    public GameObject craftingUI;
    public GameObject UIPrefab;
    public CraftingController craftCtrl;


    public Transform craftingUIContent;
    
    public KeyCode craftingKey = KeyCode.I;
    private bool isOpen;

    public CraftingController craftingController;
    private void Start()
    {
        isOpen = false;
        craftingUI.SetActive(isOpen);
        AddCraftingItemListener();
    }
    private void Update()
    {
        if (Input.GetKeyDown(craftingKey))
        {
            craftingUI.SetActive(!isOpen);           
            isOpen = !isOpen;
        } 
       
    }

    public void AddCraftingItemListener()
    {
        for (int i = 0; i < craftingUIContent.childCount; i++)
        {
            var btn = craftingUIContent.GetChild(i).GetComponent<Button>();
            btn.onClick.AddListener(delegate { craftingController.AddNeededItemToList(btn.gameObject.transform.GetSiblingIndex()); });
        }
    }

    public void HideCraftingUI()
    {
        isOpen = false;
        craftingUI.SetActive(false);       


    }
   

}
