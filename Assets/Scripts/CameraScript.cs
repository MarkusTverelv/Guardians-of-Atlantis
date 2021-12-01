using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [HideInInspector] public GameObject center;
    public bool lockCam;
    GameObject yello, pinko;
    Camera cam;
    public float delay, zoomOut;
    private void Start()
    {
        cam = GetComponent<Camera>();
        yello = GameObject.Find("Yello");
        pinko = GameObject.Find("Pinko");
    }
    private void LateUpdate()
    {
        
        if(!lockCam)
        {
            Vector2 v1 = yello.transform.position;
            Vector2 v2 = pinko.transform.position;
            Vector2 target = (v1 + v2) / 2;
            float cameraDist = Vector2.Distance(target, transform.position);
            float playerDist = Vector2.Distance(v1, v2);
            transform.position = Vector2.MoveTowards(transform.position, target, cameraDist / delay);
            transform.position += new Vector3(0, 0, -15);
            cam.orthographicSize = Mathf.Max(playerDist * zoomOut, 10);
        }
        
        
    }
}
