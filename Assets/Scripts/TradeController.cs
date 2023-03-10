using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeController : MonoBehaviour
{
    public static TradeController Instance;
    public KeyCode tradeKey = KeyCode.F;
    public TradingUI tradeUI;
    public TradingData tradingData; 

    private void Awake()
    {
        Instance = this;
    }
    public void Trade()
    {
        Debug.Log("traded");
    }

    public void SetTradingItem(GeneralItemData itemData)
    {
        tradingData.SetCurrentTradingItem(itemData);
    }

    public void Buy()
    {
      
        if (!tradingData.IsEnoughMoney()) return;
        tradingData.AddItem();
    }

    public void Sell()
    {
        if (!tradingData.IsEnoughItem()) return;
        tradingData.AddMoney();
    }
}
