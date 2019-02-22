using System;
using UnityEngine;

internal class WayPointPath
{
    public GameObject startPoint;
    public GameObject endPoint;

    private Vector3 heading;
    private float distance;

    public WayPointPath(GameObject startPoint, GameObject endPoint)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;

        heading = (endPoint.transform.position - startPoint.transform.position).normalized;
        distance = (endPoint.transform.position - startPoint.transform.position).magnitude;
    }

    public Vector3 Heading
    {
        get => heading;
    }
    public float Distance
    {
        get => distance;
    }
}