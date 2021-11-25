using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchScript : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject jellow, pinko;
    Rigidbody2D yelloBody, pinkoBody;
    //YelloScript yelloScript;
    PinkoScript pinkoScript;
    bool hold;
    void Start()
    {
        jellow = GameObject.Find("Jellow");
        //yelloScript = yellow.GetComponent<YelloScript>();
        yelloBody = jellow.GetComponent<Rigidbody2D>();
        
        pinko = GameObject.Find("Pinko");
        pinkoScript = pinko.GetComponent<PinkoScript>();
        pinkoBody = pinko.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            yelloBody.constraints = RigidbodyConstraints2D.FreezeAll;
            jellow.transform.parent = pinko.transform;

        }
    }
}
