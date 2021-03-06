﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;

internal class HighwayInTheSky
{
    private List<WayPointPath> wayPointPathList;

    public List<HITSSegment> hitsSegmentList = new List<HITSSegment>();

    private float distanceBetweenGates = 2f;

    private GameObject GatePrefab { get; }

    public HighwayInTheSky(List<WayPointPath> wayPointPathList, GameObject gatePrefab, float distanceBetweenGates)
    {
        this.wayPointPathList = wayPointPathList;
        this.distanceBetweenGates = distanceBetweenGates;
        GatePrefab = gatePrefab;
        hitsSegmentList = GenerateHITSSegments(wayPointPathList);
    }

    private List<HITSSegment> GenerateHITSSegments(List<WayPointPath> wayPointPathList)
    {
        List<HITSSegment> hitsSegments = new List<HITSSegment>();

        var distanceBetweenLastGateAndWayPointEndPoint = 0f;

        foreach (var wayPointPath in wayPointPathList)
        {
            HITSSegment previousHitsSegment = null;
            while (previousHitsSegment == null || (wayPointPath.endPoint.transform.position - previousHitsSegment.startPoint).magnitude > distanceBetweenGates )
            {
                Vector3 startRot = Vector3.left;
                if (previousHitsSegment == null)
                {
                    var startPoint = wayPointPath.startPoint.transform.position + (wayPointPath.Heading * (distanceBetweenGates-distanceBetweenLastGateAndWayPointEndPoint));
                    var endPoint = startPoint + (wayPointPath.Heading * distanceBetweenGates);
                    
                    previousHitsSegment = new HITSSegment(startPoint, endPoint, Quaternion.FromToRotation(startRot, wayPointPath.Heading));
                    hitsSegments.Add(previousHitsSegment);
                }
                else
                {
                   previousHitsSegment = new HITSSegment(previousHitsSegment.endPoint, previousHitsSegment.endPoint + (wayPointPath.Heading * distanceBetweenGates), Quaternion.FromToRotation(startRot, wayPointPath.Heading));
                   hitsSegments.Add(previousHitsSegment);
                }
            }

           distanceBetweenLastGateAndWayPointEndPoint =(wayPointPath.endPoint.transform.position - previousHitsSegment.startPoint).magnitude;


        }

        return hitsSegments;
    }

}
