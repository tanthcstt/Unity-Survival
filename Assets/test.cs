using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public GeneralItemData data;
    public List<GeneralItemData> dataList = new List<GeneralItemData>();
    private void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            data.itemCount = 2;
            dataList.Add(data);
            Debug.Log(ItemFilter.Instance.IsEnoughItems(dataList));
            Debug.Log(data.itemCount);
        } 
    }
}
