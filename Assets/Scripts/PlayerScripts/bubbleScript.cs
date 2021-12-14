using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubbleScript : MonoBehaviour
{
    GameObject pinko;
    GameObject yello;
    // Start is called before the first frame update
    void Start()
    {
        pinko = GameObject.Find("Pinko");
        yello = GameObject.Find("Yello");
        Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), pinko.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreCollision(gameObject.GetComponent<CircleCollider2D>(), yello.GetComponent<CircleCollider2D>());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
    }


}
