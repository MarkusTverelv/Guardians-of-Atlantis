using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverScript : MonoBehaviour
{
    public bool imActive;
    float leverTimer;
    private GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster");
    }

    // Update is called once per frame
    void Update()
    {

        if(imActive)
        {
            if (leverTimer < 0.5f)
            {
                leverTimer += Time.deltaTime;
                transform.Rotate(0, 0, -1);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.F))
        {
            gm.GetComponent<AudioScript>().playLeverSound();
            imActive = true;
        }
    }
}
