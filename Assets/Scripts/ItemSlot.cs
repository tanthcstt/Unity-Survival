using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject drop = eventData.pointerDrag;
        DragableItem drag = drop.GetComponent<DragableItem>();
       
        if (gameObject.transform.childCount == 0)
        {
            drag.parentAfterDrag = transform;
        }
        
    }
}
