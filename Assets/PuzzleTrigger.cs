using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    LevelChangerScript lcs;
    GameMaster gm;
    GameObject music;
    static bool hasEntered = false;
    public BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        lcs = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
        music = GameObject.Find("Music");
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
        {
            if (!hasEntered)
            {
                hasEntered = true;
                music.SetActive(false);
                gm.lastCheckPointPos = new Vector2(-38, -10);
                lcs.fadeToLevel("Puzzle_1");
            }
        }
    }

    public bool hasStaticEntered()
    {
        return hasEntered;
    }
}
