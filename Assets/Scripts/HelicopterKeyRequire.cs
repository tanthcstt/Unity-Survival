using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterKeyRequire : MonoBehaviour
{
    public GameObject key;
    public List<GeneralItemData> keyList = new List<GeneralItemData>();

    public HelicopterFly helicopterFly;
    [SerializeField] private bool isUnlock;
    private void Awake()
    {
        isUnlock = false;   
    }
    public void Unlock()
    {
        if (isUnlock)
        {
            // get in and fly
            helicopterFly.Fly();
        } else
        {
            // unlock
            keyList.Clear();    
            keyList.Add(key.GetComponent<GeneralItemData>());
            keyList[0].itemCount = 1;
            if (ItemFilter.Instance.IsEnoughItems(keyList))
            {
                isUnlock = true;
            } else
            {
                Debug.LogError("key error when passing to item filter");
            }
        }
    }

}
