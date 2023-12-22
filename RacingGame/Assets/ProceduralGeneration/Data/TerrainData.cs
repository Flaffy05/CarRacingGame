using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class TerrainData : UpdatableData
{

    public float heightMultiplier;
    public AnimationCurve heightCurve;
    public int chunkScale;
    public bool useFlatShading;
    public bool useFalloff;

    public float maxHeight 
    { 
        get { return heightMultiplier*heightCurve.Evaluate(1); } 
    }

    public float minHeight
    {
        get { return heightMultiplier * heightCurve.Evaluate(0); }
    }

}
