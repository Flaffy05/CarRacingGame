using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapChunkSize;
    public float noiseScale;
    public float scale;

    public float MaxHeight;

    public int octaves;
    [Range(0f, 1f)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;

    public AnimationCurve heightCurve;

    public TerrainType[] regions;

    public bool autoUpdate;

    public bool useFalloff;

    float[,] falloff;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapChunkSize*mapChunkSize];
        for(int y = 0; y< mapChunkSize;y++)
        {
            for(int x = 0; x< mapChunkSize; x++)
            {
                noiseMap[x,y] = heightCurve.Evaluate(noiseMap[x,y]);
                if(useFalloff)
                    noiseMap[x,y] = Mathf.Clamp01(noiseMap[x, y] - falloff[x,y]);
                

                for(int i=0; i<regions.Length; i++)
                {
                    if (noiseMap[x, y] <= regions[i].height)
                    {
                        colorMap[y*mapChunkSize+x] = regions[i].color;
                        break;
                    }
                }
                    
            }
        }

        MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
        mapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, MaxHeight),TextureGenerator.CreateTextureFromColorMap(colorMap, mapChunkSize));
    }

    private void Awake()
    {
        falloff = FalloffGenerator.GenerateFallout(mapChunkSize);
    }

    private void OnValidate()
    {
        falloff = FalloffGenerator.GenerateFallout(mapChunkSize);
        if (mapChunkSize<1)
        {
            mapChunkSize = 1;
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

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color color;
    }

}
