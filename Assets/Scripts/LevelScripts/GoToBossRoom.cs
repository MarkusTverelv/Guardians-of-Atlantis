using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToBossRoom : MonoBehaviour
{
    GameMaster gm;
    GameObject music;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        music = GameObject.Find("Music");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Pinko"))
        {
            music.SetActive(false);
            gm.lastCheckPointPos = new Vector2(-20, -4);
            SceneManager.LoadScene("Fluid");
        }
    }
}
