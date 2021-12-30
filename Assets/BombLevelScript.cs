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
    // Start is called before the first frame update
    void Start()
    {
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
                InvokeRepeating("ChangeColor", 0, 0.2f);
                Invoke("DestroyMe", 3);
            }

            if(collision.gameObject.CompareTag("Enemy"))
            {
                InvokeRepeating("ChangeColor", 0, 0.2f);
                Invoke("DestroyMe", 3);
            }

            if (collision.gameObject.CompareTag("Bomb"))
            {
                InvokeRepeating("ChangeColor", 0, 0.2f);
                Invoke("DestroyMe", 3);
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

    void DestroyMe()
    {
        int rndNmb = Random.Range(0, 3);
        GameObject explosion2 = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(bomb);
        Destroy(explosion2, 2);
    }
}
