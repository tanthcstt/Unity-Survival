/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerPooling : ObjectPooling
{
    public Transform deerPooling;
    private void Awake()
    {
        base.SetPool(deerPooling);
    }
    public void AddToPool(Transform deerSpawner)
    {
        while (deerSpawner.childCount > 0)
        {
            base.SetParent(deerSpawner.GetChild(0).gameObject);
        }
    }
}
*/