using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawner : AnimalsSpawner
{
    public WolfPooling wolfPooling;
    public GameObject wolfPrefab;
    public List<Terrain> terrainList = new List<Terrain>();
    public DayNightCycle daynightCycle;
    public bool isSpawn;
    public bool isDespawn;
   
    private void Update()
    {
        if (daynightCycle.isDay)
        {
            isSpawn = false;
            isDespawn = false;  
        }
        if (!daynightCycle.isDay && !isSpawn)
        {
            int amountToSpawn = 0;
            foreach(Terrain terrain in terrainList)
            {
                base.SetTerrain(terrain);
                base.SetNoiseMap();
                base.Spawn(wolfPrefab, transform, wolfPooling.transform);
                amountToSpawn++;    

            }
            isSpawn = true; 
            
        }
     /*   if (Input.GetKeyDown(KeyCode.P))
        {
            wolfPooling.AddToPool(transform);
        }*/
        if (daynightCycle.isDay && !isDespawn)
        {
            wolfPooling.AddToPool(transform);
            isDespawn = true;
        }

       
    }
}
