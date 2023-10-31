using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.UI;

public class Speedometer : MonoBehaviour
{
    public Transform needleTransform;

    public float minNeedleAngle;
    public float maxNeedleAngle;
    private float currentNeedleAngle = 0;

    

    public void SetNeedleAngle(float value)
    {
        //value 0-1
        Mathf.Clamp01(value);
        currentNeedleAngle = Mathf.Lerp(minNeedleAngle,maxNeedleAngle, value);
    }

    private void Update()
    {
        needleTransform.rotation = Quaternion.Euler(0,0,currentNeedleAngle);
    }
}
