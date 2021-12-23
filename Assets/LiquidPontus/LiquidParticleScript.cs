using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidParticleScript : MonoBehaviour
{
    GameObject wall;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] Tentacle = GameObject.FindGameObjectsWithTag("Tentacle");
        Physics2D.IgnoreCollision(wall.GetComponent<BoxCollider2D>(), gameObject.GetComponent<CircleCollider2D>());
        foreach (GameObject t in Tentacle)
        {
            Physics2D.IgnoreCollision(t.GetComponent<BoxCollider2D>(), GetComponent<CircleCollider2D>());
        }
        
    }
    private void Awake()
    {
        wall = GameObject.Find("BombIgnoreCollider");
    }
    // Update is called once per frame
    void Update()
    {
        Invoke("DestroyMe", 7);
    }

    void DestroyMe()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        Destroy(gameObject, 3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Yello") || collision.gameObject.CompareTag("Pinko"))
        {
            collision.gameObject.transform.parent.GetComponent<PlayerSharedScript>().TakeDamage();
        }
    }

}
