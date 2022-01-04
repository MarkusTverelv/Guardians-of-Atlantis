using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private GameMaster gm;
    bool imActive;
    public GameObject checkPointLight;
    public ParticleSystem ps;
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
                ps.Play();
                checkPointLight.GetComponent<SpriteRenderer>().color = Color.cyan;
                gm.GetComponent<AudioScript>().playCheckPointSound();
                gm.lastCheckPointPos = transform.position;
                imActive = true;
            }
        }
    }
}
