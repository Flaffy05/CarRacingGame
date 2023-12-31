using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Scripting;


public class MapChunk
{
    public GameObject meshObject;
    public Vector2 chunkCoordinates;
    public int size;

    public Vector2 chunkPosition
    {
        get { return new Vector2(meshObject.transform.position.x, meshObject.transform.position.z); }
    }

    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    private bool isActive = true;

    public MapChunk(Vector2 position, int size, Transform parent)
    {

        chunkCoordinates = position;
        this.size = size;
        this.isActive = true;

        meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //meshObject.GetComponent<Collider>().

        meshObject.transform.position = new Vector3(position.x*(size-1), 0, -position.y* (size- 1));
        //meshObject.transform.localScale = Vector3.one;
        meshObject.transform.parent = parent;

        textureRenderer = meshObject.GetComponent<MeshRenderer>();
        meshFilter = meshObject.GetComponent<MeshFilter>();
        meshRenderer = meshObject.GetComponent<MeshRenderer>();
    }

    public static bool IsActive()
    {
        return true;
    }

    public void UpdateChunk(Vector2 position, int size)
    {
        this.chunkCoordinates = position;
        this.size = size;
        meshObject.transform.position = new Vector3(position.x * (size-1), 0, -position.y * (size - 1));
        //meshObject.transform.localScale = Vector3.one;
    }


    public void SetVisible(bool visible)
    {
        isActive = visible;
        meshObject.SetActive(isActive);
    }

    public void DestroyChunk()
    {
        meshObject = null;
    }
}
