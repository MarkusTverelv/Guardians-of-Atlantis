using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarWallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float wait, speed;
    public AudioClip chaseMusic, mainMusic;
    public AudioSource musicplayer;
    public LevelChangerScript lcs;

    void Start()
    {
        lcs = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
        StartCoroutine(Chase());
    }

    IEnumerator Chase()
    {
        yield return new WaitForSeconds(wait);
        while(true)
        {   
            transform.position += new Vector3(speed, 0);
            yield return new WaitForFixedUpdate();
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Yello"))
        {
            lcs.fadeToLevel("Puzzle_1");
        }
        else if (collision.gameObject.CompareTag("Pinko"))
        {
            lcs.fadeToLevel("Puzzle_1");
        }
    }
}
