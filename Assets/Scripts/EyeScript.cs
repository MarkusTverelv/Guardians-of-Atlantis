using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    public GameObject explosion;
    private int health = 3;
    public bool invincible = false;
    private float invincibleTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(health);

        if(invincible)
        {
            invincibleTimer += Time.deltaTime;

            if(invincibleTimer > 3)
            {
                invincible = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            if (!invincible)
            {
                GameObject explosionRef = Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(explosionRef, 1);
                if (health >= 1)
                {
                    health--;
                }
                else
                {
                    Destroy(gameObject);
                }
                
                invincible = true;
                
            }
        }
    }

    
}