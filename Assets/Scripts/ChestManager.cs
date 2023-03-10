using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{    
    public static ChestManager Instance;
  
    public List<GeneralItemData> chestList;
  
    private void Awake()
    {
        Instance = this;
    }
   

    // return a  element(list)  in chestList
    public void ChestList(List<GeneralItemData> chestList)
    {
        this.chestList = chestList;            
    }

   
}
