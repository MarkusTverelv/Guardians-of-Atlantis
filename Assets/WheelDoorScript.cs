using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelDoorScript : MonoBehaviour
{
    public GameObject wall;
    public GameObject wheel, wheel2;
    public GameObject lever;
    leverScript leverScript;
    WheelScript wheelScript, wheel2Script;
    float posY;
    float originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = wall.transform.position.y;
        posY = wall.transform.position.y;
        leverScript = lever.GetComponent<leverScript>();
        wheelScript = wheel.GetComponent<WheelScript>();
        wheel2Script = wheel2.GetComponent<WheelScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (wheelScript.rotz1 > 150 && wheel2Script.rotz2 > 150 || wheel2Script.rotz1 > 150 && wheelScript.rotz2 > 150)
        {
            if(posY < originalPos + 7)
                posY += 5 * Time.deltaTime;
        }

        else
        {
            if(posY > originalPos && !leverScript.imActive)
            posY -= 1f * Time.deltaTime;
        }

        if(leverScript.imActive)
        {
            if (posY < originalPos + 7)
                posY += 5 * Time.deltaTime;
        }

        wall.transform.position = new Vector2(wall.transform.position.x, posY);

        Debug.Log(posY);
        Debug.Log(originalPos);
    }
}
