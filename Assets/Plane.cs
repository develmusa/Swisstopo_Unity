using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{

    public GameObject StartingPoint;

    public float speed = 1f;
    public float turnSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = StartingPoint.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0,0,-1);
    }
}
