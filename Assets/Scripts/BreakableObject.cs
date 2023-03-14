using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Scriptabe_Obj", menuName = "BreakableObject/Create Object")]
public class BreakableObject : ScriptableObject
{
    public string ObjName;
    public int ObjID;
    public GameObject DropObj;
    public bool isHaveBrokenObj;
    public GameObject BrokenObj;
    // damage if it is animal
    public int damage;
}
