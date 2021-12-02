using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBond : MonoBehaviour
{
    //LineRenderer lr;
    List<Transform> linePositions = new List<Transform>();

    public bool visiable, horizontal;
    public int lineLenght;
    public GameObject lineSegment;
    //List<GameObject> lineSegments;
    GameObject jellow, pinko;
    

    // Start is called before the first frame update
    void Start()
    {
        //lr = GetComponent<LineRenderer>();
        //lr.positionCount = lineLenght;
        List<GameObject> lineSegments = new List<GameObject>();
        for (int i = 0; i < lineLenght; i++)
        {   
            if(horizontal)
                lineSegments.Add(Instantiate(lineSegment, transform.position + new Vector3(0, i / 10f), Quaternion.identity, transform));
            else
                lineSegments.Add(Instantiate(lineSegment, transform.position + new Vector3(i / 10f, 0), Quaternion.identity, transform));
            lineSegments[i].name = "Linesegment " + i;
            SpriteRenderer spriteRenderer = lineSegments[i].GetComponent<SpriteRenderer>();
            spriteRenderer.enabled = visiable;
            Color yellow = Color.white;
            yellow.r = 252 / 255f;
            yellow.g = 1;
            yellow.b = 121 / 255f;
            Color pink = Color.white;
            pink.r = 229 / 255f;
            pink.g = 172 / 255f;
            pink.b = 180 / 255f;
            
            spriteRenderer.color = Color.Lerp(yellow, pink, (float)i/lineLenght);
            
            linePositions.Add(lineSegments[i].transform);
            if (i != 0)
                lineSegments[i].GetComponent<HingeJoint2D>().connectedBody = lineSegments[i - 1].GetComponent<Rigidbody2D>();
            if (i == lineLenght / 2)
            {
                GameObject Camera = GameObject.Find("Main Camera");
                CameraScript cameraScript = Camera.GetComponent<CameraScript>();
                cameraScript.center = lineSegments[i];
                if (!cameraScript.lockCam)
                    Camera.transform.position = lineSegments[i].transform.position += new Vector3(0, 0, -15); 
            }
                
        }
        jellow = gameObject.transform.parent.transform.Find("Yello").gameObject;
        pinko = gameObject.transform.parent.transform.Find("Pinko").gameObject;
        jellow.GetComponent<HingeJoint2D>().connectedBody = lineSegments[0].GetComponent<Rigidbody2D>();
        lineSegments[0].GetComponent<HingeJoint2D>().connectedBody = jellow.GetComponent<Rigidbody2D>();
        jellow.transform.position = lineSegments[0].transform.position;
        pinko.GetComponent<HingeJoint2D>().connectedBody = lineSegments[lineLenght - 1].GetComponent<Rigidbody2D>();
        pinko.transform.position = lineSegments[lineLenght - 1].transform.position;
        //SpringJoint2D spring = jellow.AddComponent(typeof(SpringJoint2D)) as SpringJoint2D;
        //spring.connectedBody = pinko.GetComponent<Rigidbody2D>();
        //spring.dampingRatio = 0.5F;
        
        //StartCoroutine(centerPlayers());

    }

    //IEnumerator centerPlayers()
    //{
    //    yield return new WaitForFixedUpdate();
    //    pinko.GetComponent<Rigidbody2D>().AddForce(new Vector2(-80, 0), ForceMode2D.Impulse);
    //}

    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    for (int i = 0; i < linePositions.Count; i++)
    //    {
    //        lr.SetPosition(i,  linePositions[i].position);
    //    }
    //}
}
