using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[CreateAssetMenu]
public class TextureData : UpdatableData
{
    public Color flatColor;
    public Color steepColor;

    public Color[] colors;

    float savedMinHeight;
    float savedMaxHeight;

    public void ApplyToMaterial(Material material)
    {
        

        material.SetColor("flatColor", flatColor);
        material.SetColor("steepColor", steepColor);

        material.SetInt("numberOfColors", colors.Length);
        material.SetColorArray("colorArray", colors);
        
        UpdateMeshHeights(material, savedMinHeight, savedMaxHeight);
    }

    public void UpdateMeshHeights(Material material, float minHeight, float maxHeight)
    {
        savedMaxHeight = maxHeight;
        savedMinHeight = minHeight;

        material.SetFloat("minHeight", minHeight);
        material.SetFloat("maxHeight", maxHeight);
    }
}
