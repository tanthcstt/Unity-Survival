using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public Transform pool;
    private void Start()
    {
      
    }
    public void SetParent(GameObject obj)
    {
        obj.transform.SetParent(this.pool);
        obj.SetActive(false);

    } 

   /* public void GetItemFromList(GameObject obj, List<GameObject> pool)
    {

    }*/
    public void GetFromPool()
    {

    }
    protected void SetPool(Transform pool)
    {
        this.pool = pool;
    }

  
}
