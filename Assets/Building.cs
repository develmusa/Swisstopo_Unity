using System;
using UnityEngine;

internal class Building
{
    private double? longitude;
    private double? latitude;
    private string modelLocation;

    private GameObject model;

    public Building(double? longitude, double? latitude, string modelLocation)
    {
        this.model = Resources.Load<GameObject>(modelLocation);
        this.longitude = longitude;
        this.latitude = latitude;
        this.modelLocation = modelLocation;
        
    }

    public double? Longitude { get => longitude;}

    public double? Latitude{get => latitude;}

    public GameObject Model
    {
        get => model;
    }
}