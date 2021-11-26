using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<SpriteRenderer>().enabled)
        {
            transform.Rotate(Vector3.forward, -Input.GetAxisRaw("Horizontal") * 2);
        }

        else
        {
            return;
        }
    }
}
