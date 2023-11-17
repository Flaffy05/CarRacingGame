using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int numberOfChunks;

    public int chunkSize;
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
        for (int j = 0; j < numberOfChunks; j++) 
        {
            for (int x = 0; x < numberOfChunks; x++)
            {

                Vector2 chunkPosOffset = new Vector2(x, j);
                Vector2 chunkNoiseOffset = new Vector2(x,j);
                GenerateChunk(chunkPosOffset, chunkNoiseOffset);
            }
        }
    }

    private void GenerateChunk(Vector2 chunkPosOffset, Vector2 chunkNoiseOffset)
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(chunkSize, chunkSize, seed, noiseScale, octaves, persistance, lacunarity, chunkNoiseOffset+ offset);

        Color[] colorMap = new Color[chunkSize * chunkSize];
        for (int y = 0; y < chunkSize; y++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                noiseMap[x, y] = heightCurve.Evaluate(noiseMap[x, y]);

                //redo
                if (useFalloff)
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloff[x, y]);


                for (int i = 0; i < regions.Length; i++)
                {
                    if (noiseMap[x, y] <= regions[i].height)
                    {
                        colorMap[y * chunkSize + x] = regions[i].color;
                        break;
                    }
                }

            }
        }

        MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
        mapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, MaxHeight, chunkPosOffset), TextureGenerator.CreateTextureFromColorMap(colorMap, chunkSize), chunkPosOffset, chunkSize);
    }

    private void Awake()
    {
        falloff = FalloffGenerator.GenerateFallout(chunkSize);
    }

    private void OnValidate()
    {
        falloff = FalloffGenerator.GenerateFallout(chunkSize);
        if (chunkSize<1)
        {
            chunkSize = 1;
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
