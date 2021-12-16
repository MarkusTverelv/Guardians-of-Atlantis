using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorRuneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("ScaleIndicator", 0, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        float scale = transform.localScale.x;
        //if(scale >= 100)
        //{
        //}

    }

    void ScaleIndicator()
    {
        transform.localScale += new Vector3(0.25f, 0.25f);
    }
}
