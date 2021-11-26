using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBond : MonoBehaviour
{
    LineRenderer lr;
    List<Transform> linePositions = new List<Transform>();

    public bool visiable;
    public int lineLenght;
    public GameObject lineSegment;
    List<GameObject> lineSegments;

    // Start is called before the first frame update
    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = lineLenght;
        List<GameObject> lineSegments = new List<GameObject>();
        for (int i = 0; i < lineLenght; i++)
        {
            lineSegments.Add(Instantiate(lineSegment, transform.position + new Vector3(i / 10f, 0), Quaternion.identity, transform));
            lineSegments[i].name = "Linesegment " + i;
            lineSegments[i].GetComponent<SpriteRenderer>().enabled = visiable;
            linePositions.Add(lineSegments[i].transform);
            if (i != 0)
                lineSegments[i].GetComponent<HingeJoint2D>().connectedBody = lineSegments[i - 1].GetComponent<Rigidbody2D>();
            if (i == lineLenght / 2)
                lineSegments[i].tag = "Center";
        }
        GameObject jellow = GameObject.Find("Yello");
        GameObject pinko = GameObject.Find("Pinko");
        jellow.GetComponent<HingeJoint2D>().connectedBody = lineSegments[0].GetComponent<Rigidbody2D>();
        lineSegments[0].GetComponent<HingeJoint2D>().connectedBody = jellow.GetComponent<Rigidbody2D>();
        jellow.transform.position = lineSegments[0].transform.position;
        pinko.GetComponent<HingeJoint2D>().connectedBody = lineSegments[lineLenght - 1].GetComponent<Rigidbody2D>();
        pinko.transform.position = lineSegments[lineLenght - 1].transform.position;


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < linePositions.Count; i++)
        {
            lr.SetPosition(i, linePositions[i].position);
        }
    }
}
