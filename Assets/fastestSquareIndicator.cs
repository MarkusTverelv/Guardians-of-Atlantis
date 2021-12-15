using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fastestSquareIndicator : MonoBehaviour
{
    Color colorShifter;
    float colorTransparancy;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.7f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (colorTransparancy < 0.80f)
        {
            colorTransparancy += 0.01f;
            colorShifter = new Color(160, 0, 255, colorTransparancy);
            gameObject.GetComponent<SpriteRenderer>().color = colorShifter;
        }
    }
}