using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddDashScript : MonoBehaviour
{
    PlayerSharedScript pss;
    public static bool doIExist = true;
    public GameObject yellowExplosion;
    public Text dashText;
    // Start is called before the first frame update
    void Start()
    {
        if (!doIExist)
        {
            gameObject.active = false;
        }

        pss = GameObject.Find("NewPlayers").GetComponent<PlayerSharedScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pss.addDash();
        doIExist = false;
        dashText.GetComponent<TextScript>().textAnimate();
        Instantiate(yellowExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
        
    }
}
