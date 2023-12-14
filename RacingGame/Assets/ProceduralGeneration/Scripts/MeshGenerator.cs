using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap,float maxHeight, int chunkLod)
    {
        int size = heightMap.GetLength(0);
        //int size = heightMap.GetLength(1);
        
        int topLeftX = ((size - 1) / -2);
        int topLeftZ = (size - 1) / 2;

        int meshSimplificationIncrement = (chunkLod==0) ? 1 : chunkLod*2;
        int verticePerLine = (size - 1) / meshSimplificationIncrement + 1;

        MeshData meshData = new MeshData(size);
        int vertexIndex = 0;

        for (int y = 0; y < size; y+= meshSimplificationIncrement)
        {
            for(int x = 0; x < size; x+= meshSimplificationIncrement)
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
        
        //meshData.FlatShading();

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
        //FlatShading();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        //mesh.RecalculateNormals();
        mesh.normals = CalculateNormals();
        //FlatShading();
        return mesh;
    }

    public Vector3[] CalculateNormals()
    {
        Vector3[] vertexNormals = new Vector3[vertices.Length];
        int triangleCount = triangles.Length/3;

        for(int i= 0; i<triangleCount; i++)
        {
            int normalTriangleIndex = i * 3;
            int vertexIndexA = triangles[normalTriangleIndex];
            int vertexIndexB = triangles[normalTriangleIndex + 1];
            int vertexIndexC = triangles[normalTriangleIndex + 2];

            Vector3 triangleNormal = SurfaceNormalsFromIndices(vertexIndexA, vertexIndexB, vertexIndexC);

            vertexNormals[vertexIndexA] = triangleNormal;
            vertexNormals[vertexIndexB] = triangleNormal;
            vertexNormals[vertexIndexC] = triangleNormal;
        }

        for(int i= 0; i< vertexNormals.Length; i++)
        {
            vertexNormals[i].Normalize();
        }

        return vertexNormals;
    }

    private Vector3 SurfaceNormalsFromIndices(int indexA, int indexB, int indexC)
    {
        Vector3 pointA = vertices[indexA];
        Vector3 pointB = vertices[indexB];
        Vector3 pointC = vertices[indexC];

        Vector3 sideAB = pointB - pointA;
        Vector3 sideAC = pointC - pointA;

        return Vector3.Cross(sideAB, sideAC ).normalized;
    }


    public void FlatShading()
    {
        Vector3[] flatShadedVertices = new Vector3[triangles.Length];
        Vector2[] flatShadedUvs = new Vector2[triangles.Length];

        for(int i = 0; i < triangles.Length; i++)
        {
            flatShadedVertices[i] = vertices[triangles[i]];
            flatShadedUvs[i] = uvs[triangles[i]];
            triangles[i] = i;
        }
        vertices = flatShadedVertices;
        uvs = flatShadedUvs;


    }

}
