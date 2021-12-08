using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EelSprite : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject body;
    Rigidbody2D sBody;
    Rigidbody2D Rbody;
    void Start()
    {
        sBody = body.GetComponent<Rigidbody2D>();
        Rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Rbody.velocity = sBody.velocity;
        transform.rotation = body.transform.rotation;
    }
}
