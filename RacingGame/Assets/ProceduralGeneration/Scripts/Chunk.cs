using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MapChunk
{
    public GameObject meshObject;
    public Vector2 position;
    public int size;

    public Renderer textureRenderer;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;

    public MapChunk(Vector2 position, int size)
    {

        this.position = position;
        this.size = size;

        //meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
        //meshObject.GetComponent<Collider>().
        meshObject.transform.position = new Vector3(position.x*(size-1), 0, position.y* (size- 1));
        //meshObject.transform.localScale = Vector3.one;
        //meshObject.transform.parent = parent;

        textureRenderer = meshObject.GetComponent<MeshRenderer>();
        meshFilter = meshObject.GetComponent<MeshFilter>();
        meshRenderer = meshObject.GetComponent<MeshRenderer>();
    }

    public void UpdateChunk(Vector2 position)
    {
        this.position = position;
        //this.size = size;
        meshObject.transform.position = new Vector3(position.x * (size-1), 0, position.y * (size - 1));
        //meshObject.transform.localScale = Vector3.one;
    }

    



    public void SetVisible(bool visible)
    {
        meshObject.SetActive(visible);
    }
}
