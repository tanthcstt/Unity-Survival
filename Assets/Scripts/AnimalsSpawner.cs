using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using UnityEngine.AI;

public class AnimalsSpawner : MonoBehaviour
{
    public float density;
    public int width;
    public int height;
    protected float scale = 25f;
    public Terrain spawnRange;
    public Texture2D noiseMap;
    public List<Terrain> terrainList = new List<Terrain>();
    public DayNightCycle daynightCycle;
    public Texture2D NoiseMap()
    {
        width = (int)spawnRange.terrainData.size.x;
        height = (int)spawnRange.terrainData.size.y;    
        Texture2D noiseMap = new Texture2D(width, height); 

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                float noiseValue = Mathf.PerlinNoise((float)i / width * scale, (float)j / height * scale);
                noiseMap.SetPixel(i, j, new Color(0f, noiseValue, 0f));
            }
        }
        noiseMap.Apply();
        return noiseMap;
    }
    
    
    public float NoiseMapValue(int x, int z)
    {
        return (float)noiseMap.GetPixel(x,z).g; 
    }
    public Vector3 SpawnPosition(int x, int z)
    {
        Vector3 pos = new Vector3(spawnRange.transform.position.x + x + Random.Range(-5f, 5f), 0f, spawnRange.transform.position.z + z + Random.Range(-5f, 5f));
        pos.y = spawnRange.SampleHeight(new Vector3(pos.x, 0f, pos.z));
        return pos;
    }
    public void Spawn(GameObject animalPrefab, Transform parent, Transform pool)
    {
       
        for (int x = 0; x < spawnRange.terrainData.size.x; x += 7)
        {
            for (int z = 0; z < spawnRange.terrainData.size.z; z += 7)
            {
                if (NoiseMapValue(x,z) > 1 - density)
                {
                   
                    if (pool.childCount == 0)
                    {
                        Vector3 spawnPos = SpawnPosition(x, z);
                        if (!IsAgentOnNavMesh(spawnPos)) return;
                        // instantiate
                        Instantiate(animalPrefab, spawnPos,Quaternion.identity ,parent);
                    } else
                    {
                        // set avtive && pos
                        GameObject animal = pool.GetChild(0).gameObject;                       
                        animal.SetActive(true);
                        animal.transform.SetParent(parent);
                        animal.transform.position = SpawnPosition(x, z);
                    }
                                    
                    
                   
                }
            }
        }
    }
    public void SetTerrain(Terrain terrain)
    {
        spawnRange = terrain;
    }
    public void SetNoiseMap()
    {
        this.noiseMap = NoiseMap();
    }
    public bool IsAgentOnNavMesh(Vector3 pos)
    {
        if (NavMesh.SamplePosition(pos, out NavMeshHit hit, 1f, NavMesh.AllAreas)) {
            return true;
        }
        return false;
    }
  
}
