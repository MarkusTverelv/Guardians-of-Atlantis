using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameMaster gm;
    bool imActive;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Pinko"))
        {
            if (!imActive)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                gm.GetComponent<AudioScript>().playCheckPointSound();
                gm.lastCheckPointPos = transform.position;
                imActive = true;
            }
        }
    }
}
