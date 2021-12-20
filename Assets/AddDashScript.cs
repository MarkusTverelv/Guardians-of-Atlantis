using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDashScript : MonoBehaviour
{
    PlayerSharedScript pss;
    // Start is called before the first frame update
    void Start()
    {
        pss = GameObject.Find("NewPlayers").GetComponent<PlayerSharedScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pss.addDash();
        Destroy(gameObject);
    }
}
