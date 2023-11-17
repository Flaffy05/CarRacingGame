using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapDisplay : MonoBehaviour
{
    public Transform chunkParent;
    public Material mapMaterial; 

    //public int chunkNumber;

    //public Chunk[] chunks;
    //public List<Chunk> chunks = new List<Chunk>();
    Dictionary<Vector2, Chunk> chunkDictionay = new Dictionary<Vector2, Chunk>();
    //public List<Chunk> chunkList = new List<Chunk>(); 
    
    

    public void DrawMesh(MeshData meshData, Texture2D texture, Vector2 chunkPosition, int chunkSize)
    {

        if (chunkDictionay.ContainsKey(chunkPosition))
        {
            //do things
            chunkDictionay[chunkPosition].UpdateChunk(chunkPosition, chunkSize);
            chunkDictionay[chunkPosition].textureRenderer.sharedMaterial = mapMaterial;
            chunkDictionay[chunkPosition].textureRenderer.sharedMaterial.mainTexture = texture;//1
            chunkDictionay[chunkPosition].meshFilter.sharedMesh = meshData.CreateMesh();
            
            //chunkDictionay[chunkPosition].meshRenderer.sharedMaterial = mapMaterial; 
        }
        else
        {   //create chunk
            chunkDictionay.Add(chunkPosition, new Chunk(chunkPosition, chunkSize, chunkParent));
        }
        //if(true)
        //{
        //    chunkDictionay.Remove(chunkPosition);
        //}

    }




    /*
    public void DrawNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colors = new Color[width * height];

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                colors[y*width+x] = Color.Lerp(Color.black, Color.white, noiseMap[x,y]);
            }
        }

        texture.SetPixels(colors);
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3((float)width, 1, (float)height);

    }*/
}
