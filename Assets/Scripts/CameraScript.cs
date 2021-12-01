using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [HideInInspector] public GameObject center;

    
    private void LateUpdate()
    {
        float distance = Mathf.Pow(Vector2.Distance(transform.position, center.transform.position),5);
        Vector2 newPos = Vector2.MoveTowards(transform.position, center.transform.position, distance *10);
        transform.position = (Vector3)newPos + new Vector3(0, 0, transform.position.z);
    }
}
