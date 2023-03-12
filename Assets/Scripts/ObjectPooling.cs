using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public static ObjectPooling instance;
    public Transform pool;
    private void Awake()
    {
        instance = this;    
    }



    public void SetPool(Transform pool)
    {
        this.pool = pool;
    }

    public void AddToPool(Transform spawner)
    {
        while (spawner.childCount > 0)
        {
            spawner.GetChild(0).gameObject.SetActive(false);
            spawner.GetChild(0).transform.SetParent(this.pool);
        }
    }


}
