using UnityEngine;


public class MapDisplay : MonoBehaviour
{
    public Transform chunkParent;
     

    public int chunkNumber;//number of chunks in a side of a grid of chunks

    

    public MapChunk[] mapChunks;
    //public List<Chunk> chunks = new List<Chunk>();
    //Dictionary<Vector2, Chunk> chunkDictionay = new Dictionary<Vector2, Chunk>();
    //public List<Chunk> chunkList = new List<Chunk>(); 
    
    

    public void DrawMesh(MeshData meshData, Vector2 chunkPosition)
    {
        MapGenerator mapGenerator = FindObjectOfType<MapGenerator>();
        chunkNumber = mapGenerator.numberOfChunks;
        if(mapChunks == null || mapChunks.Length != chunkNumber*chunkNumber)  
        {
            
            mapChunks = new MapChunk[chunkNumber * chunkNumber];

        }

        if (mapChunks[(int)chunkPosition.x+(int)chunkPosition.y* chunkNumber] == null)
        {
            mapChunks[(int)chunkPosition.x + (int)chunkPosition.y * chunkNumber] = new MapChunk(chunkPosition, mapGenerator.chunkSize, chunkParent);
        }
        
        mapChunks[(int)chunkPosition.x + (int)chunkPosition.y * chunkNumber].UpdateChunk(chunkPosition, mapGenerator.chunkSize);
        mapChunks[(int)chunkPosition.x + (int)chunkPosition.y * chunkNumber].textureRenderer.sharedMaterial = mapGenerator.terrainMaterial;
        //mapChunks[(int)chunkPosition.x + (int)chunkPosition.y * chunkNumber].textureRenderer.sharedMaterial.mainTexture = texture;
        mapChunks[(int)chunkPosition.x + (int)chunkPosition.y * chunkNumber].meshFilter.sharedMesh = meshData.CreateMesh();
        

            

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
