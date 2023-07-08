using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingLine : MonoBehaviour
{
    //points in fishing line
    public Transform start;
    public Transform mid;
    public Transform end;

    //line renderer variables
    public LineRenderer line;
    public float vertexCount = 12;

    //mid offset
    public float midOffset = 2;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        mid.transform.position = new Vector3((start.position.x + end.position.x) / 2,
                                             midOffset,
                                             (start.position.z + end.position.z / 2));
        var pointList = new List<Vector3>();

        for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
        {
            var tangent1 = Vector3.Lerp(start.localPosition, mid.localPosition, ratio);
            var tangent2 = Vector3.Lerp(mid.localPosition, mid.parent.InverseTransformPoint(end.position), ratio);
            var curve = Vector3.Lerp(tangent1, tangent2, ratio);

            pointList.Add(curve);
        }

        line.positionCount = pointList.Count;
        line.SetPositions(pointList.ToArray());

    }

}
