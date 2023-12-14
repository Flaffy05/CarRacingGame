using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Collections.Generic;


public class MapGenerator : MonoBehaviour
{
    public MapDisplay mapDisplay;
    public Material terrainMaterial;

    public TerrainData terrainData;
    public NoiseData noiseData;
    public TextureData textureData;

    public int numberOfChunks;
    public const int chunkSize = 241;
    [Range(0, 6)]
    public int chunkLod = 1;

    public bool autoUpdate;

    float[,] falloff;

    public float[,] noiseMap;


    public void GenerateMap()
    {
        //mapDisplay = FindObjectOfType<MapDisplay>();
        //mapDisplay.mapChunks = new MapChunk[numberOfChunks* numberOfChunks];

        noiseMap = Noise.GenerateNoiseMap(numberOfChunks * (chunkSize - 1)+1, noiseData.seed, noiseData.noiseScale, noiseData.octaves, noiseData.persistance, noiseData.lacunarity, noiseData.offset);

        for (int y = 0; y < numberOfChunks * (chunkSize - 1) + 1; y++)
        {
            for (int x = 0; x < numberOfChunks * (chunkSize - 1) + 1; x++)
            {
                noiseMap[x, y] = terrainData.heightCurve.Evaluate(noiseMap[x, y]);

                
                if (terrainData.useFalloff)
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloff[x, y]);
            }
        }

        textureData.UpdateMeshHeights(terrainMaterial, terrainData.minHeight, terrainData.maxHeight);

        for (int y = 0; y < numberOfChunks; y++) 
        {
            for (int x = 0; x < numberOfChunks; x++)
            {

                Vector2 chunkPosOffset = new Vector2(x, y);
                //Vector2 chunkNoiseOffset = new Vector2(x,y);
                GenerateChunk(chunkPosOffset);
            }
        }
    }

    private void GenerateChunk(Vector2 chunkPosOffset)
    {
        
        float[,] chunkNoiseMap = new float[chunkSize,chunkSize];

        for (int y = 0; y < chunkSize; y++)
        {
            for (int x = 0; x < chunkSize; x++)
            {
                chunkNoiseMap[x,y] = noiseMap[x+(int)chunkPosOffset.x*(chunkSize-1), y + (int)chunkPosOffset.y * (chunkSize - 1)]; 
            }
        }


        //MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
        mapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(chunkNoiseMap, terrainData.heightMultiplier, chunkLod), chunkPosOffset);
    }

    private void Awake()
    {
        falloff = FalloffGenerator.GenerateFallout(numberOfChunks * (chunkSize - 1) + 1);
    }

    private void OnValuesUpdated()
    {
        if(!Application.isPlaying)
        {
            GenerateMap();
            OnTextureValuesUpdated();
        }
    }

    private void OnTextureValuesUpdated()
    {
        textureData.ApplyToMaterial(terrainMaterial);
    }


    private void OnValidate()
    {
        falloff = FalloffGenerator.GenerateFallout(numberOfChunks * (chunkSize - 1) + 1);
        if(terrainData != null)
        {
            terrainData.OnValuesUpdated -= OnValuesUpdated;
            terrainData.OnValuesUpdated += OnValuesUpdated;
        }
        if (noiseData != null)
        {
            noiseData.OnValuesUpdated -= OnValuesUpdated;
            noiseData.OnValuesUpdated += OnValuesUpdated;
        }
        if (textureData != null)
        {
            textureData.OnValuesUpdated -= OnValuesUpdated;
            textureData.OnValuesUpdated += OnValuesUpdated;
        }

    }

   

}
