using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class TerrainData : UpdatableData
{
    //public int numberOfChunks;

    public float heightMultiplier;
    public AnimationCurve heightCurve;
    //public int chunkSize;
    public bool useFlatShading;
    public bool useFalloff;

}
