using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
using Unity.Mathematics;
using UnityEngine.UI;

#if UNITY_EDITOR
public class ObjectPlacementProcedural : EditorWindow
{
    public Texture2D noiseMapTexture;
    public float density = 0.5f;
    public Terrain activeTerrain;
    public GameObject prefab;
    public GameObject envObj;
    public Vector3 terrainPosition;

    //create item menu
    [MenuItem("Tools/ProceduralPlacementObj/TreePlacement")]
       
    public static void ShowWindow() 
    {
        GetWindow<ObjectPlacementProcedural>();
       
    }
    // 
    private void OnGUI()
    {
        activeTerrain = (Terrain)EditorGUILayout.ObjectField("Terrain ", activeTerrain, typeof(Terrain), true);
        envObj = (GameObject)EditorGUILayout.ObjectField("Parent ", envObj, typeof(GameObject), true);
        noiseMapTexture = (Texture2D)EditorGUILayout.ObjectField("Noise Map", noiseMapTexture, typeof(Texture2D), true);
       
        if (GUILayout.Button("Generate Noise Map"))
        {
            // noise map
            int width = (int)activeTerrain.terrainData.size.x;
            int height = (int)activeTerrain.terrainData.size.y;
            float scale = 25f;
            noiseMapTexture = NoiseMapGenerator.NoiseMap(width, height, scale);
        }

       

        // prefab
        prefab = (GameObject)EditorGUILayout.ObjectField("Object to place", prefab, typeof(GameObject), true);
    
        


        density = EditorGUILayout.Slider("Density", density, 0f, 1f);
        if (GUILayout.Button("Place Object"))
        {
            //place object
            PlaceObj(activeTerrain, noiseMapTexture, density, prefab, envObj.transform); 
        }
     
    }

    public static void PlaceObj(Terrain terrain, Texture2D noiseMap, float density, GameObject prefab, Transform parent)
    {
        for (int x = 0; x < terrain.terrainData.size.x; x+= 7)
        {
            for (int z = 0; z < terrain.terrainData.size.z; z+= 7)
            {
                if (PixelVal(noiseMap, x,z, terrain) > 1 - density)
                {
                    Vector3 position = new Vector3(terrain.transform.position.x + x + Random.Range(-5f, 5f), 0f, terrain.transform.position.z + z + Random.Range(-5f,5f));
                    position.y = terrain.SampleHeight(new Vector3(position.x, 0f, position.z));
                    GameObject obj = Instantiate(prefab, position, quaternion.identity);

                    Vector3 scaleChange = (float)Random.Range(0.5f, 3.5f) * obj.transform.lossyScale;
                    obj.transform.localScale = scaleChange;
                    obj.transform.localScale = scaleChange;
                    obj.transform.SetParent(parent);
                }
            }
        }
    }

    private static float PixelVal(Texture2D noiseMap, int x, int z, Terrain terrain)
    {
        
        float pixelVal = noiseMap.GetPixel(x, z).g;
        float steepness = terrain.terrainData.GetSteepness(x / terrain.terrainData.size.x, z / terrain.terrainData.size.z);
        pixelVal += Random.Range(-0.25f, 0.25f);
        if (steepness >= 30)
        {
            pixelVal = 0f;
        }
        return pixelVal;    
    }
}
#endif