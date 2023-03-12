using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSpawner : AnimalsSpawner
{
 
    public GameObject wolfPrefab;
    public Transform wolfPooling;
    public bool isSpawned;
    public bool isDespawned;
   
    private void Update()
    {
        if (daynightCycle.isDay)
        {
            isSpawned = false;
            isDespawned = false;  
        }
        if (!daynightCycle.isDay && !isSpawned)
        {
            
            foreach(Terrain terrain in terrainList)
            {
                base.SetTerrain(terrain);
                base.SetNoiseMap();
                base.Spawn(wolfPrefab, transform, wolfPooling);
                

            }
            isSpawned = true; 
            
        }
     /*   if (Input.GetKeyDown(KeyCode.P))
        {
            wolfPooling.AddToPool(transform);
        }*/
        if (daynightCycle.isDay && !isDespawned)
        {
            ObjectPooling.instance.SetPool(wolfPooling);
            ObjectPooling.instance.AddToPool(transform);
            isDespawned = true;
        }

       
    }
}
