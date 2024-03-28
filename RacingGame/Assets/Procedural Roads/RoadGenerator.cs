using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    Vector3 pointA;
    Vector3 pointB;

    //roadRequirements


    public float[,] heightMap;

    public void GenerateRoadPoints(Vector3 pointA, Vector3 pointB, List<Vector3> roadPoints, float[,] heightMap, float accuracy, int roadPointsNum)
    {
        Vector3 bDirection = pointB - pointA;
        float bDistance = bDirection.magnitude;

        //int pointsNum = Mathf.FloorToInt(accuracy / bDistance);
        int pointsNum = roadPointsNum;

        roadPoints[0] = pointA;

        for(int i=0; i<pointsNum; i++)
        {
            Vector3 currentPoint = roadPoints[i];
            currentPoint.y = heightMap[(int)currentPoint.x, (int)currentPoint.z];
            //Vector3 nextPoint;
            Vector3[] candidatePoints = new Vector3[32];
            bool[] pointCanBeUsed = new bool[32];
            for(int j=0; j<32; j++)
            {
                int offsetX = (int)Mathf.Cos(2 * Mathf.PI / 32 * j);
                int offsetZ = (int)Mathf.Sin(2 * Mathf.PI / 32 * j);
                candidatePoints[j] = currentPoint+ new Vector3(offsetX, heightMap[offsetX, offsetZ], offsetZ);

                pointCanBeUsed[j] = checkRequirements(currentPoint, candidatePoints[j]);
            }

        }

    }

    private bool checkRequirements(Vector3 pointA, Vector3 pointB)
    {
        float deltaY = pointA.y - pointB.y;
        Vector3 bDirection = pointB - pointA;
        float distance = bDirection.magnitude;

        if (deltaY < distance / 10)
            return true;

        return false;
    }


}
