using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddDashScript : MonoBehaviour
{
    AudioScript audioScript;
    PlayerSharedScript pss;
    public static bool doIExist = true;
    public static bool doIExist2 = true;
    public static bool doIExist3 = true;
    public GameObject yellowExplosion;
    public Text dashText;
    public int dashNmbr;
    public GameObject dashGem;
    public GameObject dashGem2;
    public GameObject dashGem3;
    // Start is called before the first frame update
    void Start()
    {
        audioScript = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioScript>();

        if (!doIExist)
        {
            dashGem.GetComponent<SpriteRenderer>().enabled = false;
            dashGem.GetComponent<CircleCollider2D>().enabled = false;
        }
        if (!doIExist2)
        {
            dashGem2.GetComponent<SpriteRenderer>().enabled = false;
            dashGem2.GetComponent<CircleCollider2D>().enabled = false;
        }
        if (!doIExist3)
        {
            dashGem3.GetComponent<SpriteRenderer>().enabled = false;
            dashGem3.GetComponent<CircleCollider2D>().enabled = false;
        }


        pss = GameObject.Find("NewPlayers").GetComponent<PlayerSharedScript>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (dashNmbr == 1)
        {
            audioScript.playDashSound();
            pss.AddDash();
            doIExist = false;
            dashText.GetComponent<TextScript>().textAnimate();
            Instantiate(yellowExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (dashNmbr == 2)
        {
            audioScript.playDashSound();
            pss.AddDash();
            doIExist2 = false;
            dashText.GetComponent<TextScript>().textAnimate();
            Instantiate(yellowExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        if (dashNmbr == 3)
        {
            audioScript.playDashSound();
            pss.AddDash();
            doIExist3 = false;
            dashText.GetComponent<TextScript>().textAnimate();
            Instantiate(yellowExplosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

    }
}
