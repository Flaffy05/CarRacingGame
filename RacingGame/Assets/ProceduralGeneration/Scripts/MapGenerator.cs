using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float scale;

    public int octaves;
    [Range(0f, 1f)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;

    public bool autoUpdate;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, scale, octaves, persistance, lacunarity, offset);

        MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
        mapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap),null);
    }

    private void OnValidate()
    {
        if(mapWidth<1)
        {
            mapWidth = 1;
        }
        if(mapHeight<1)
        {
            mapHeight = 1;
        }
        if(octaves<0)
        {
            octaves = 0;
        }
        if(lacunarity <1)
        {
            lacunarity = 1;
        }
    }

}
