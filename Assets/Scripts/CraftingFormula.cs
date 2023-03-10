using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.HighDefinition;
using UnityEngine;


public class CraftingFormula : MonoBehaviour
{
    public static CraftingFormula Instance;
    public List<GeneralItemData> allCraftingMaterials = new List<GeneralItemData>();
    public List<GameObject> allCraftedItems = new List<GameObject>();
    private void Awake()
    {
        Instance = this;
    }
    public Dictionary<int, Dictionary<int, int>> craftingFormula = new Dictionary<int, Dictionary<int, int>>()
    {
        {5, campFire },
        {0, axe },
        {1, pikaxe },
        {7, bow },
        {8, arrow }
    };


    public static Dictionary<int, int> campFire = new Dictionary<int, int>() // materials ID , amount
    {
        {6, 1 }, // 1 stick
        {4, 3 }// 3 stone
    };

    public static Dictionary<int, int> axe = new Dictionary<int, int>()
    {
        {6, 1 }, // 1 stick
        {4, 1 },// 1 stone
        {5, 1 }, // rope
    };
    public static Dictionary<int, int> pikaxe = new Dictionary<int, int>()
    {
        {6, 1 }, // 1 stick
        {4, 3 },// 3 stone
        {5, 1 }, // rope
    };
    public static Dictionary<int, int> bow = new Dictionary<int, int>()
    {
        {6, 3 }, // 1 stick
        {5, 1 }, // rope
    };
    public static Dictionary<int, int> arrow = new Dictionary<int, int>()
    {
        {6, 1 }, // 1 stick
        {4, 1 },// 1 stone
    };

   


}
