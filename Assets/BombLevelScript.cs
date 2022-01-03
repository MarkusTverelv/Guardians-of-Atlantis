using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLevelScript : MonoBehaviour
{
    SpriteRenderer bombSpriteRenderer;
    Color colorShifter;
    bool changeBool = true;
    public GameObject bomb;
    public GameObject explosion;
    private GameObject gm;
    bool notActivated;
    public GameObject[] hinges;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster");
        bombSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        {
            if(collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
            {
                if (!notActivated)
                {
                    InvokeRepeating("ChangeColor", 0, 0.2f);
                    StartCoroutine(DestroyMe());
                    notActivated = true;
                }
            }

            if(collision.gameObject.CompareTag("Enemy"))
            {
                if (!notActivated)
                {
                    InvokeRepeating("ChangeColor", 0, 0.2f);
                    StartCoroutine(DestroyMe());
                    notActivated = true;
                }
            }

            if (collision.gameObject.CompareTag("Bomb"))
            {
                if (!notActivated)
                {
                    InvokeRepeating("ChangeColor", 0, 0.2f);
                    StartCoroutine(DestroyMe());
                    notActivated = true;
                }
            }

        }
    }


    void ChangeColor()
    {
        switch (changeBool)
        {
            case true:
                colorShifter = new Color(255, 0, 0);
                changeBool = false;
                break;
            case false:
                colorShifter = new Color(255, 255, 255);
                changeBool = true;
                break;
            default:
                break;
        }
        bombSpriteRenderer.color = colorShifter;
    }


    public IEnumerator DestroyMe()
    {
        gm.GetComponent<AudioScript>().playExplodeSound();
        yield return new WaitForSeconds(2f);
        GameObject explosion2 = Instantiate(explosion, transform.position, Quaternion.identity);
        foreach(GameObject hinge in hinges)
        {
            hinge.GetComponent<Rigidbody2D>().gravityScale = 1;
            Destroy(hinge, 3);
        }
        Destroy(gameObject);
    }

}
