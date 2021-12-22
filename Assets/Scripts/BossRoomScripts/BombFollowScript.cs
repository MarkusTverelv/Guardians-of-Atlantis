using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombFollowScript : MonoBehaviour
{
    GameObject pinko;
    GameObject yello;
    GameObject[] tentacles;
    SpriteRenderer bombSpriteRenderer;
    float pinkoPos;
    float yelloPos;
    float activateTimer;
    float pushActivateTimer;
    public bool imActivated;
    bool imReallyActivated;
    bool activated;
    bool changeBool;
    Color colorShifter;
    public GameObject[] explosionPrefab;
    // Start is called before the first frame update
    void Start()
    {
        tentacles = GameObject.FindGameObjectsWithTag("BigTentacle");
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        pinko = GameObject.Find("Pinko");
        yello = GameObject.Find("Yello");
        bombSpriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            activateTimer += Time.deltaTime;

            if (activateTimer > 3)
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
                foreach (GameObject tentacle in tentacles)
                {
                    Physics2D.IgnoreCollision(tentacle.GetComponent<BoxCollider2D>(), gameObject.GetComponent<CircleCollider2D>());
                }
                activated = true;
            }
        }
        pinkoPos = (pinko.transform.position - transform.position).magnitude;
        yelloPos = (yello.transform.position - transform.position).magnitude;

        if (pinkoPos < yelloPos)
        {
            transform.position = Vector2.MoveTowards(transform.position, pinko.transform.position, 2 * Time.deltaTime);
        }

        else
        {
            transform.position = Vector2.MoveTowards(transform.position, yello.transform.position, 2 * Time.deltaTime);
        }

        if(imActivated)
        {
            InvokeRepeating("ChangeColor", 0, 0.2f);
            pushActivateTimer += Time.deltaTime;
            if (pushActivateTimer > 0.2f)
            {
                imReallyActivated = true;
            }
        }

        if (imReallyActivated)
        {
            foreach (GameObject tentacle in tentacles)
            {
                Physics2D.IgnoreCollision(tentacle.GetComponent<BoxCollider2D>(), gameObject.GetComponent<CircleCollider2D>(), false);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int rndNmb = Random.Range(0, 3);

        if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
        {
            GameObject explosion = Instantiate(explosionPrefab[rndNmb], transform.position, Quaternion.identity);
            collision.gameObject.transform.parent.GetComponent<PlayerSharedScript>().TakeDamage();
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

        if (collision.gameObject.CompareTag("Wall"))
        {
            GameObject explosion = Instantiate(explosionPrefab[rndNmb], transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(explosion, 2);
        }

        if (collision.gameObject.CompareTag("Bomb"))
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
}
