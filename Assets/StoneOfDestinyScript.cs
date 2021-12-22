using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneOfDestinyScript : MonoBehaviour
{
    static bool imFound = false;
    public int wallNmbr;
    public GameObject stone;
    public GameObject stone1;
    public GameObject stone2;
    public Text stoneText;
    // Start is called before the first frame update
    void Start()
    {
        if(imFound)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (wallNmbr == 1)
        {
            if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
            {
                imFound = true;
                stoneText.GetComponent<TextScript>().textAnimate();
                stone.GetComponent<StoneOfDestinyWallScript>().stopExisting();
                Destroy(gameObject);
            }
        }

        if (wallNmbr == 2)
        {
            if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
            {
                imFound = true;
                stoneText.GetComponent<TextScript>().textAnimate();
                stone1.GetComponent<stoneOfDestinyWall2>().stopExisting();
                Destroy(gameObject);
            }
        }

        if (wallNmbr == 3)
        {
            if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
            {
                imFound = true;
                stoneText.GetComponent<TextScript>().textAnimate();
                stone2.GetComponent<StoneOfDestinyWall3>().stopExisting();
                Destroy(gameObject);
            }
        }
    }
}
