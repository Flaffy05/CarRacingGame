using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[CreateAssetMenu]
public class TextureData : UpdatableData
{
    public Color flatColor;
    public Color steepColor;

    float savedMinHeight;
    float savedMaxHeight;

    public void ApplyToMaterial(Material material)
    {
        //do stuff
        //material.SetInt("_baseColorsCount", baseColors.Length);
        //material.SetColorArray("_baseColors", b6444863
        //material.SetFloatArray("_baseHeights", baseHeights);

        material.SetColor("_flatColor", flatColor);
        material.SetColor("_steepColor", steepColor);

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
