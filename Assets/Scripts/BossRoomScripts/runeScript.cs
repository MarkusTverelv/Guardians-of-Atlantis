using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runeScript : MonoBehaviour
{

    public int myNumber;
    private BossScript boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
        {
            if(myNumber == boss.runesActivated)
            {
                boss.runesActivated++;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<CircleCollider2D>().enabled = false;
            }

            else
            {

            }

        }
    }
}
