using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeerSpawner : AnimalsSpawner
{
    public Transform deerPooling;
    public bool isSpawned;
    public bool isDespawned;
    public GameObject deerPrefab;
    private void Update()
    {
        if (base.daynightCycle.isDay && !isSpawned)
        {
            //spawn
            foreach (Terrain terrain in terrainList)
            {
                base.SetTerrain(terrain);
                base.SetNoiseMap();
                base.Spawn(deerPrefab, transform, deerPooling);


            }
            isSpawned = true;   
            isDespawned = false;    
        } else if (!base.daynightCycle.isDay && !isDespawned)
        {
            // despawn 
            ObjectPooling.instance.SetPool(deerPooling);
            ObjectPooling.instance.AddToPool(transform);
            isSpawned = false;
            isDespawned = true;
        }
    }
}
