using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TradingUI : MonoBehaviour
{
    public GameObject tradingUI;
    public Transform tradingUIContent;
    public Button sellBtn;
    public Button buyBtn;
    public GameObject sprite;
    private void Awake()
    {
        tradingUI.SetActive(false);
        AddListener();
    }
    private void Start()
    {
        
    }
    public void AddListener()
    {
        buyBtn.onClick.AddListener(BuyBtnOnClick);
        sellBtn.onClick.AddListener(SellBtnOnClick);
    }
    public void BuyBtnOnClick()
    {
        TradeController.Instance.Buy();
    }
    public void SellBtnOnClick()
    {
        TradeController.Instance.Sell();
    }

}
