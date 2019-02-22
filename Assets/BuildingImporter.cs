
using System;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = System.Object;

public class BuildingImporter : MonoBehaviour
{

    public GameObject BuildingContainerGameObject;
    public double x;
    public double y;

    private List<Building> _buildings;

    private string buildingSaveLocation =
        "Assets/Resources/Buildings/KML/Musterdaten_swissBUILDINGS3D20_WGS84_1166-42/";
    //"Assets/Buildings/KML/Musterdaten_swissBUILDINGS3D20_WGS84_1166-42/models/model.dae";

    private string kmlFileName = "swissBUILDINGS3D20_1166-42.kml";

    private string havkstring = "Buildings/KML/Musterdaten_swissBUILDINGS3D20_WGS84_1166-42/models/";

    private double minLat = Double.MaxValue;
    private double minLong = Double.MaxValue;

    

    void Start()
    {
        _buildings = new List<Building>();
        KmlFile file;
        string path = buildingSaveLocation + kmlFileName;
        using (StreamReader reader = new StreamReader(path))
        {
            file = KmlFile.Load(reader);
            Debug.Log("loaded");
        }

        var kml = file.Root as Kml;
        if (kml != null)
        {
            Debug.Log("not null");
            foreach (var placemark in kml.Flatten().OfType<Placemark>())
            {
                Debug.Log(placemark.Name);
                foreach (var model in placemark.Flatten().OfType<Model>())
                {
                    var lat = model.Location.Latitude;
                    var lon = model.Location.Longitude;
                    if (lat < minLat)
                    {
                        minLat = lat.Value;
                    }

                    if (lon < minLong)
                    {
                        minLong = lon.Value;
                    }

                    _buildings.Add(new Building(model.Location.Longitude, model.Location.Latitude, havkstring + Path.GetFileNameWithoutExtension(model.Link.Href.ToString())));
                }
            }
        }

        PlaceBuildings();



    }

    private void PlaceBuildings()
    {
        Debug.Log("lat" + minLat);
        Debug.Log("long" + minLong);

        foreach (var building in _buildings)
        {
            //https://en.wikipedia.org/wiki/Geographic_coordinate_system
            var mPerDegLat = 111132.92 - 559.82 * Math.Cos(2 * minLat) + 1.175 * Math.Cos(4 * minLat) - 0.0023 * Math.Cos(6 * minLat);
            var mPerDegLon = 111412.84 * Math.Cos(minLong) - 93.5 * Math.Cos(3 * minLong) + 0.118 * Math.Cos(5 * minLong);



            var posLatInM = (building.Latitude - minLat) * mPerDegLat;
            var posLonInM = (building.Longitude - minLong) * mPerDegLon * 1.65;
            GameObject buildingGameObject = Instantiate(building.Model, new Vector3((float)(posLonInM.Value + 624 - 455- 190 ) , 54 - 33 - 15, (float)(posLatInM.Value) + 1628 - 623 - 14), Quaternion.Euler(-90, 0, 180));

            buildingGameObject.transform.parent = BuildingContainerGameObject.transform;
        }
    }



    //Update is called once per frame
    void Update()
    {
        
    }


}
