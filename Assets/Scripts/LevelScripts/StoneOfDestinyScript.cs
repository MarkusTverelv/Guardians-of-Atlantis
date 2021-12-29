using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class StoneOfDestinyScript : MonoBehaviour
{
    static bool imFound = false;
    static bool imFound2 = false;
    static bool imFound3 = false;
    public int wallNmbr;
    public GameObject stone;
    public GameObject stone1;
    public GameObject stone2;
    public GameObject destinyStone;
    public GameObject destinyStone1;
    public GameObject destinyStone2;
    public Text stoneText;
    private GameObject gm;
    private LevelChangerScript lcs;
    public Transform beginning;
    GameObject music;
    float changeLevelTimer;
    bool goToBeginning;
    // Start is called before the first frame update
    void Start()
    {
        
        music = GameObject.Find("Music");
        lcs = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
        gm = GameObject.Find("GameMaster");
        if(imFound)
        {
            destinyStone.GetComponent<Light2D>().enabled = false;
            destinyStone.GetComponent<SpriteRenderer>().enabled = false;
            destinyStone.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (imFound2)
        {
            destinyStone1.GetComponent<Light2D>().enabled = false;
            destinyStone1.GetComponent<SpriteRenderer>().enabled = false;
            destinyStone1.GetComponent<CircleCollider2D>().enabled = false;
        }
        if (imFound3)
        {
            destinyStone2.GetComponent<Light2D>().enabled = false;
            destinyStone2.GetComponent<SpriteRenderer>().enabled = false;
            destinyStone2.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (goToBeginning)
        {
            changeLevelTimer += Time.deltaTime;
            if (changeLevelTimer > 5)
            {
                gm.GetComponent<GameMaster>().lastCheckPointPos = beginning.position;
                music.SetActive(false);
                lcs.fadeToLevel("Level");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (wallNmbr == 1)
        {
            if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
            {
                goToBeginning = true;
                gm.GetComponent<AudioScript>().playDestinySound();
                imFound = true;
                stoneText.GetComponent<TextScript>().textAnimate();
                stone.GetComponent<StoneOfDestinyWallScript>().stopExisting();
                destinyStone.GetComponent<Light2D>().enabled = false;
                destinyStone.GetComponent<SpriteRenderer>().enabled = false;
                destinyStone.GetComponent<CircleCollider2D>().enabled = false;
            }
        }

        if (wallNmbr == 2)
        {
            if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
            {
                goToBeginning = true;
                gm.GetComponent<AudioScript>().playDestinySound();
                imFound2 = true;
                stoneText.GetComponent<TextScript>().textAnimate();
                stone1.GetComponent<stoneOfDestinyWall2>().stopExisting();
                destinyStone1.GetComponent<Light2D>().enabled = false;
                destinyStone1.GetComponent<SpriteRenderer>().enabled = false;
                destinyStone1.GetComponent<CircleCollider2D>().enabled = false;
            }
        }

        if (wallNmbr == 3)
        {
            if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
            {
                goToBeginning = true;
                gm.GetComponent<AudioScript>().playDestinySound();
                imFound3 = true;
                stoneText.GetComponent<TextScript>().textAnimate();
                stone2.GetComponent<StoneOfDestinyWall3>().stopExisting();
                destinyStone2.GetComponent<Light2D>().enabled = false;
                destinyStone2.GetComponent<SpriteRenderer>().enabled = false;
                destinyStone2.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }
}
