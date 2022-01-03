using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrenceScript : MonoBehaviour
{
    public LevelChangerScript lcs;
    public GameMaster gm;
    public string SceneName;

    private void Start()
    {
        gm = GameObject.Find("GameMaster").GetComponent<GameMaster>();
        lcs = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
        {
            collision.transform.parent.GetComponent<PlayerSharedScript>().AddDash();
            collision.transform.parent.GetComponent<PlayerSharedScript>().AddMaxHealth();
            gm.lastCheckPointPos = new Vector2(20, -48);
            lcs.fadeToLevel("Level");
        }

    }
}
