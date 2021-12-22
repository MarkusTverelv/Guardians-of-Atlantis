using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public GameObject[] explosionPrefab;
    SpriteRenderer bombSpriteRenderer;
    Color colorShifter;
    bool changeBool = true;
    private BossScript boss;
    Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        bombSpriteRenderer = GetComponent<SpriteRenderer>();
        boss = GameObject.Find("Boss").GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Instantiate(gameObject, new Vector3(mousePos.x, mousePos.y, 0), Quaternion.identity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int rndNmb = Random.Range(0, 3);

        if (collision.gameObject.CompareTag("Wall"))
        {
            GameObject explosion = Instantiate(explosionPrefab[rndNmb], transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(explosion, 2);
        }

        if (collision.gameObject.CompareTag("Gem"))
        {
            InvokeRepeating("ChangeColor", 0, 0.2f);
            Invoke("DestroyMe", 3);
        }

        if (collision.gameObject.CompareTag("Tentacle"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            collision.gameObject.GetComponent<TentacleScript>().MoveDown();
            GameObject explosion = Instantiate(explosionPrefab[rndNmb], transform.position, Quaternion.identity);
            boss.amountOfTentaclesKilled++;
            Debug.Log(boss.amountOfTentaclesKilled);
            Destroy(gameObject);
            Destroy(explosion, 2);
        }

        if (collision.gameObject.CompareTag("BigTentacle"))
        {
            Destroy(gameObject);
            GameObject explosion = Instantiate(explosionPrefab[rndNmb], transform.position, Quaternion.identity);
            collision.gameObject.GetComponent<EyeTentacles>().health--;
            Destroy(explosion, 2);
        }

        if (collision.gameObject.CompareTag("Oil"))
        {
            GameObject explosion = Instantiate(explosionPrefab[rndNmb], transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(explosion, 1);
        }

        if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
        {
            GameObject explosion = Instantiate(explosionPrefab[rndNmb], transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(explosion, 2);
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
        GameObject explosion = Instantiate(explosionPrefab[rndNmb], transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(explosion, 2);
    }
}
