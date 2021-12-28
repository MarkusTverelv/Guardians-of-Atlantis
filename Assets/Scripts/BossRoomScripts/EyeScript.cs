using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeScript : MonoBehaviour
{
    public GameObject explosion;
    public int health = 3;
    public bool invincible = false;
    private float invincibleTimer;
    BossScript boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<BossScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincible)
        {
            invincibleTimer += Time.deltaTime;

            if(invincibleTimer > 3)
            {
                invincible = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            if (!invincible)
            {
                boss.playTentacleDamageSound();
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
