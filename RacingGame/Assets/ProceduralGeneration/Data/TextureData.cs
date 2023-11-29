using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[CreateAssetMenu]
public class TextureData : UpdatableData
{
    public Color[] baseColors;
    [Range(0,1)]
    public float[] baseHeights;


    float savedMinHeight;
    float savedMaxHeight;

    public void ApplyToMaterial(Material material)
    {
        //do stuff
        material.SetInt("_baseColorsCount", baseColors.Length);
        material.SetColorArray("_baseColors", baseColors);
        material.SetFloatArray("_baseHeights", baseHeights);


        UpdateMeshHeights(material, savedMinHeight, savedMaxHeight);
    }

    public void UpdateMeshHeights(Material material, float minHeight, float maxHeight)
    {
        savedMaxHeight = maxHeight;
        savedMinHeight = minHeight;

        material.SetFloat("_minHeight", minHeight);
        material.SetFloat("_maxHeight", maxHeight);
    }
}
