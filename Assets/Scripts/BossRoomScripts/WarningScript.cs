using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningScript : MonoBehaviour
{
    Color colorShifter;
    bool changeBool = true;
    SpriteRenderer indicatorRenderer;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        indicatorRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("ChangeColor", 0, 0.4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeColor()
    {
        switch (changeBool)
        {
            case true:
                colorShifter = new Color(255, 0, 0);
                changeBool = false;
                break;
            case false:
                colorShifter = new Color(255, 255, 255);
                changeBool = true;
                break;
            default:
                break;
        }
        indicatorRenderer.color = colorShifter;
    }

}
