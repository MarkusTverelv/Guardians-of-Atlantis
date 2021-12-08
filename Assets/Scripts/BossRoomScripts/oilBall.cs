using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oilBall : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Grow();
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Grow()
    {
        transform.localScale = transform.localScale * 2;
        Invoke("GrowMore", 1);
    }

    void GrowMore()
    {
        transform.localScale = transform.localScale * 2;
        
    }

}
