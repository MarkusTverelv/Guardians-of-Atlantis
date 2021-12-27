using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddDashScript : MonoBehaviour
{
    AudioScript audioScript;
    PlayerSharedScript pss;
    public static bool doIExist = true;
    public GameObject yellowExplosion;
    public Text dashText;
    // Start is called before the first frame update
    void Start()
    {
        audioScript = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioScript>();

        if (!doIExist)
        {
            gameObject.active = false;
        }
        GetComponent<SpriteRenderer>().enabled = false;

        pss = GameObject.Find("NewPlayers").GetComponent<PlayerSharedScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioScript.playDashSound();
        pss.AddDash();
        doIExist = false;
        dashText.GetComponent<TextScript>().textAnimate();
        Instantiate(yellowExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
