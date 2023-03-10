using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingBtnHOver : MonoBehaviour, IPointerClickHandler
{
    protected Image itemSprite;
    protected Text itemName;
    protected Text itemCount;
    protected Image itemDiscriptionImage;
    protected Text itemPrice;
   
    private void Awake()
    {
        GetComponent();       

    }
    private void Start()
    {
        LoadUIInfor();
    }
    public void OnPointerEnter()
    {
       
    }
    public void GetComponent()
    {
        itemSprite = transform.Find("ItemSprite").GetComponent<Image>();
        itemName = transform.Find("ItemName").GetComponent<Text>();
        itemCount = transform.Find("Count").GetComponent <Text>();

        itemDiscriptionImage = GameObject.Find("UI/TradingUI/Scroll View/SellAndBuy/Sprite").GetComponent<Image>();
        itemPrice = GameObject.Find("UI/TradingUI/Scroll View/SellAndBuy/Price").GetComponent<Text>();

    }
    public void LoadUIInfor()
    {
        GeneralItemData itemInfo = gameObject.GetComponent<GeneralItemData>();
        itemSprite.sprite = itemInfo.item.itemSprite;
        itemName.text = itemInfo.item.itemName;
        itemCount.text = itemInfo.itemCount.ToString();
    }
    public void ShowItemDiscription()
    {
        itemDiscriptionImage.sprite = itemSprite.sprite;     
        itemPrice.text = gameObject.GetComponent<GeneralItemData>().item.price.ToString();
 
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        ShowItemDiscription();
        TradeController.Instance.SetTradingItem(gameObject.GetComponent<GeneralItemData>());
    }
}
