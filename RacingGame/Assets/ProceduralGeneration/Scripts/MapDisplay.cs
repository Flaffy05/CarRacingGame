using UnityEngine;


public class MapDisplay : MonoBehaviour
{
    public Transform chunkParent;
    public Material mapMaterial; 

    //public int chunkNumber;
    
    public MapChunk[] mapChunks;
    //public List<Chunk> chunks = new List<Chunk>();
    //Dictionary<Vector2, Chunk> chunkDictionay = new Dictionary<Vector2, Chunk>();
    //public List<Chunk> chunkList = new List<Chunk>(); 
    
    

    public void DrawMesh(MeshData meshData, Texture2D texture, Vector2 chunkPosition)
    {
        if (mapChunks[(int)chunkPosition.x+(int)chunkPosition.y ]== null)
        {
            mapChunks[(int)chunkPosition.x + (int)chunkPosition.y * (int)Mathf.Sqrt(mapChunks.Length)] = new MapChunk(chunkPosition, 10, chunkParent);
        }
        else
        {
            mapChunks[(int)chunkPosition.x + (int)chunkPosition.y].UpdateChunk(chunkPosition);
            mapChunks[(int)chunkPosition.x + (int)chunkPosition.y].textureRenderer.sharedMaterial = mapMaterial;
            mapChunks[(int)chunkPosition.x + (int)chunkPosition.y].textureRenderer.sharedMaterial.mainTexture = texture;
            mapChunks[(int)chunkPosition.x + (int)chunkPosition.y].meshFilter.sharedMesh = meshData.CreateMesh();
        }

            

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
