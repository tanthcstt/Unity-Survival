using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject menu;
    public GameObject invetoryManager;
    public KeyCode toggleMenu = KeyCode.B;
    public bool isOpen;

    private void Start()
    {
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
    private void Update()
    {
        if (Input.GetKeyDown(toggleMenu))
        {
            Toggle();
        }
    }
    public void Toggle()
    {
        isOpen = !isOpen;   
        menu.SetActive(isOpen);
        if (isOpen)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
       
    }
}
