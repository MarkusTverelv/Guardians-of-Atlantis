using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{ 
    public GameObject[] explosionPrefab;
    SpriteRenderer bombSpriteRenderer;
    Color colorShifter;
    bool changeBool = true;
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
        int rndNmb = Random.Range(0, 3);

        if (collision.gameObject.CompareTag("Wall"))
        {
            Instantiate(explosionPrefab[rndNmb], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }

        if(collision.gameObject.CompareTag("Gem"))
        {
            InvokeRepeating("ChangeColor", 0, 0.2f);
            Invoke("DestroyMe", 3);
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
        Instantiate(explosionPrefab[rndNmb], transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
