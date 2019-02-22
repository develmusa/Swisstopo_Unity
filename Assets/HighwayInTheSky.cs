using System;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

internal class HighwayInTheSky
{
    private List<WayPointPath> wayPointPathList;

    public List<HITSSegment> hitsSegmentList = new List<HITSSegment>();

    public float distanceBetweenGates = 2f;

    public GameObject GatePrefab { get; }

    public HighwayInTheSky(List<WayPointPath> wayPointPathList, GameObject gatePrefab)
    {
        this.wayPointPathList = wayPointPathList;
        GatePrefab = gatePrefab;
        hitsSegmentList = GenerateHITSSegments(wayPointPathList);
    }

    private List<HITSSegment> GenerateHITSSegments(List<WayPointPath> wayPointPathList)
    {
        List<HITSSegment> hitsSegments = new List<HITSSegment>();
        foreach (var wayPointPath in wayPointPathList)
        {
            HITSSegment previousHitsSegment = null;
            while (previousHitsSegment == null || (wayPointPath.endPoint.transform.position - previousHitsSegment.startPoint).magnitude > distanceBetweenGates )
            {
                Vector3 startRot = Vector3.left;
                if (previousHitsSegment == null)
                {
                    var endPoint = wayPointPath.startPoint.transform.position + (wayPointPath.Heading * distanceBetweenGates);
                    var startPoint = wayPointPath.startPoint.transform.position;
                    previousHitsSegment = new HITSSegment(startPoint, endPoint, Quaternion.FromToRotation(startRot, wayPointPath.Heading));
                    hitsSegments.Add(previousHitsSegment);
                }
                else
                {
                   previousHitsSegment = new HITSSegment(previousHitsSegment.endPoint, previousHitsSegment.endPoint + (wayPointPath.Heading * distanceBetweenGates), Quaternion.FromToRotation(startRot, wayPointPath.Heading));
                   hitsSegments.Add(previousHitsSegment);
                }
            }
        }

        return hitsSegments;
    }

}
