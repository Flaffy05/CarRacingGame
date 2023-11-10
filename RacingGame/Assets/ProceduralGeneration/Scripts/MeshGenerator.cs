using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap,float maxHeight)
    {
        int size = heightMap.GetLength(0);
        //int size = heightMap.GetLength(1);
        
        int topLeftX = (size - 1) / -2;
        int topLeftZ = (size - 1) / 2;

        MeshData meshData = new MeshData(size);
        int vertexIndex = 0;

        for (int y = 0; y < size; y++)
        {
            for(int x = 0; x < size; x++)
            {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX+x, heightMap[x, y]*maxHeight, topLeftZ-y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)size, y / (float)size);


                if(x<size-1&&y<size-1)
                {
                    meshData.AddTriangle(vertexIndex, vertexIndex + size + 1, vertexIndex + size);
                    meshData.AddTriangle(vertexIndex + size + 1, vertexIndex,vertexIndex + 1);
                }

                vertexIndex++;
            }
        }
        return meshData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;

    public MeshData(int meshSize)
    {
        vertices = new Vector3[meshSize * meshSize];
        triangles = new int[(meshSize-1) * (meshSize-1)*6];
        uvs = new Vector2[meshSize * meshSize];
    }
    
    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex+1] = b;
        triangles[triangleIndex+2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }

}
