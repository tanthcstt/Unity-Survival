using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipUI : MonoBehaviour
{
    public GameObject fixShipUI;

    public void FixShipUIToggle(bool UIState)
    {
        fixShipUI.SetActive(UIState);   
        if (UIState)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } 
      
    }
}
