using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float speed = 0.01f;
    Vector3 startPos;
    private void Start()
    {
        startPos = transform.position;   
    }
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.O))
        //    Open();
    }
    public void Open()
    {
        StartCoroutine(Opening());
    }
    IEnumerator Opening()
    {
        while(transform.position.y<startPos.y+10)
        {
            transform.position += new Vector3(0, speed);
            yield return new WaitForFixedUpdate();
        }
    }
}
