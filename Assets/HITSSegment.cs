using UnityEngine;

internal class HITSSegment
{
    public Vector3 startPoint;
    public Vector3 endPoint;

    public HITSSegment(Vector3 startPoint, Vector3 endPoint, Quaternion direction)
    {
        this.startPoint = startPoint;
        this.endPoint = endPoint;
        Direction = direction;
    }

    public Quaternion Direction { get; }
}