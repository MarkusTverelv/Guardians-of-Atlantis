using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelDoorScript : MonoBehaviour
{
    AudioScript audioScript;
    public GameObject wall;
    public GameObject wheel, wheel2;
    public GameObject lever;
    public ParticleSystem pe;
    public ParticleSystem pe1;
    public ParticleSystem pe2;
    leverScript leverScript;
    WheelScript wheelScript, wheel2Script;
    float posY;
    float originalPos;
    bool startedPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        audioScript = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioScript>();
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
            if (posY < originalPos + 7)
            {
                posY += 5 * Time.deltaTime;
                pe.Play();
                pe1.Play();
                pe2.Play();
            }
            if (startedPlaying == false)
                {
                    audioScript.stoneSound();
                    startedPlaying = true;
                }
            
                
        }

        else
        {
            if (posY > originalPos && !leverScript.imActive)
            {
                posY -= 1f * Time.deltaTime;
                pe.Stop();
                pe1.Stop();
                pe2.Stop();
            }
            if(startedPlaying == true)
            {
                audioScript.stopStoneSound();
                audioScript.stoneSound();
                startedPlaying = false;
            }
        }

        if(leverScript.imActive)
        {
            if (posY < originalPos + 7)
                posY += 5 * Time.deltaTime;
        }

        

        wall.transform.position = new Vector2(wall.transform.position.x, posY);
    }
}
