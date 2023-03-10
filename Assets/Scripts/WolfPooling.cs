using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfPooling : ObjectPooling
{
    public Transform wolfPooling;
    private void Awake()
    {
        base.SetPool(wolfPooling);  
    }
    public void AddToPool(Transform wolfSpawner)
    {
        while (wolfSpawner.childCount > 0)
        {
            base.SetParent(wolfSpawner.GetChild(0).gameObject);
        }
    }

   
}
