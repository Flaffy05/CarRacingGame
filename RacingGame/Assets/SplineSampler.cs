using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Splines;

[ExecuteInEditMode]
public class SplineSampler : MonoBehaviour
{

    [SerializeField]
    public SplineContainer splineContainer;

    [SerializeField]
    private int splineIndex;

    [SerializeField]
    private float width;
    [SerializeField]
    private float resolution;

    [SerializeField]
    [Range(0f, 1f)]
    private float time;

    private float3 position;
    private float3 tangent;
    private float3 upVector;

    float3 p1;
    float3 p2;

    public List<Vector3> VerticesP1 = new List<Vector3>();
    public List<Vector3> VerticesP2 = new List<Vector3>();

    // Update is called once per frame
    void Update()
    {
        splineContainer.Evaluate(splineIndex, time, out position, out tangent, out upVector);

        float3 right = Vector3.Cross(tangent, upVector).normalized;
        p1 = position + (right*width);
        p2 = position + (-right * width);
    }

    private void OnDrawGizmos()
    {
        Handles.matrix = transform.localToWorldMatrix;
        Handles.SphereHandleCap(0, p1, Quaternion.identity, 1f, EventType.Repaint);
        Handles.SphereHandleCap(0, p2, Quaternion.identity, 1f, EventType.Repaint);
    }
}
