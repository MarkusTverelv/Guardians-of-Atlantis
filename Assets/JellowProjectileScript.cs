using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellowProjectileScript : MonoBehaviour
{
    
    private Transform attachPoint;
    
    

    // Start is called before the first frame update
    void Start()
    {
       
        attachPoint = GameObject.Find("JellowAttach").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, attachPoint.position, 5 * Time.deltaTime);
    }
}
