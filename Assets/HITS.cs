using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HITS : MonoBehaviour
{
    public GameObject Waypoints;
    public GameObject GatePrefab;

    private List<GameObject> WaypointsList;

    private List<WayPointPath> wayPointPathList;

    private HighwayInTheSky highwayInTheSky;


    // Start is called before the first frame update
    void Start()
    {
        WaypointsList = new List<GameObject>();
        wayPointPathList = new List<WayPointPath>();
        
        for (int i = 0; i < Waypoints.transform.childCount; i++)
        {
            WaypointsList.Add(Waypoints.transform.GetChild(i).gameObject);
        }
        SetWayPointPaths(WaypointsList);

        highwayInTheSky = new HighwayInTheSky(wayPointPathList, GatePrefab);

        foreach (var hitsSegment in highwayInTheSky.hitsSegmentList)
        {
            Instantiate(GatePrefab, hitsSegment.startPoint, hitsSegment.Direction);
        }


        Debug.DrawLine(WaypointsList[1].transform.position, WaypointsList[2].transform.position, Color.blue, 10f, true);      
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetWayPointPaths(List<GameObject> waypointsList)
    {
        GameObject previousWaypoint = null;

        foreach (var waypoint in waypointsList)
        {
            if (previousWaypoint == null)
            {
                previousWaypoint = waypoint;
                continue;
            }
            
            wayPointPathList.Add(new WayPointPath(previousWaypoint, waypoint));
            previousWaypoint = waypoint;
        }
    }
}
