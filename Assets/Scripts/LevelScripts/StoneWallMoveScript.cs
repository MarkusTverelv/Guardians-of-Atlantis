using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneWallMoveScript : MonoBehaviour
{
    private AudioScript gm;
    public GameObject lever, lever2, lever3, leverfinish;
    leverScript leverScript, leverScript2, leverScript3, leverfinishScript;
    public GameObject moveSpot;
    bool playSound;
    bool hasPlayedSound;
    public Text stoneText;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<AudioScript>();
        leverScript = lever.GetComponent<leverScript>();
        leverScript2 = lever2.GetComponent<leverScript>();
        leverScript3 = lever3.GetComponent<leverScript>();
        leverfinishScript = leverfinish.GetComponent<leverScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(leverScript.imActive && leverScript2.imActive && leverScript3.imActive)
        {
            playSound = true;
            stoneText.GetComponent<TextScript>().textAnimate();
            transform.position = Vector2.MoveTowards(transform.position, moveSpot.transform.position, 2 * Time.deltaTime);
        }

        if(leverfinishScript.imActive)
        {
            playSound = true;
            transform.position = Vector2.MoveTowards(transform.position, moveSpot.transform.position, 2 * Time.deltaTime);
        }

        if(playSound && !hasPlayedSound)
        {
            gm.stoneSound();
            playSound = false;
            hasPlayedSound = true;
        }
    }
}
