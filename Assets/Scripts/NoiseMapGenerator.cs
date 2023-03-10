using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseMapGenerator 
{
    // width  = x coordinate
    // height = y coordinate
    public static Texture2D NoiseMap(int width, int height, float scale) 
    {
        Texture2D noiseMap = new Texture2D(width, height);    

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++ )
            {
                float noiseValue = Mathf.PerlinNoise((float)i / width * scale, (float)j / height * scale );
                noiseMap.SetPixel(i, j, new Color(0f, noiseValue, 0f));
            }
        }
        noiseMap.Apply();   
        return noiseMap;
    }
}
