using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [HideInInspector] public Transform parentAfterDrag;
    public Image itemUI;
    [HideInInspector]public Transform UIMenu;
    [HideInInspector]public Transform parentBeforeDrag;
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        parentAfterDrag = transform.parent;
        parentBeforeDrag = transform.parent;
        UIMenu = parentAfterDrag.parent;       
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        itemUI.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        itemUI.raycastTarget = true;
        InventoryManager.Instance.ChangeStorageListController(parentBeforeDrag, parentAfterDrag);


    }
}
