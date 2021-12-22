using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleScript : MonoBehaviour
{
    float posY;
    public bool ImActive = false;
    public BossScript boss;
    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("Boss").GetComponent<BossScript>();
        posY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (ImActive)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            Move();
            Invoke("MoveDown", 10);
        }

        if(!ImActive)
        {
            MoveDown();
        }

        this.transform.position = new Vector2(transform.position.x, posY);

       
    }

    private void Move()
    {
        if(posY < 5)
        posY += 0.1f;
    }

    public void MoveDown()
    {
        ImActive = false;
        if (posY > -30)
            posY -= 0.1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Yello") || collision.gameObject.CompareTag("Pinko"))
        {
            collision.gameObject.transform.parent.GetComponent<PlayerSharedScript>().TakeDamage();
        }
        
        if (collision.gameObject.CompareTag("Bomb"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            MoveDown();
            //GameObject explosion = Instantiate(explosionPrefab[rndNmb], transform.position, Quaternion.identity);
            boss.amountOfTentaclesKilled++;
            Debug.Log(boss.amountOfTentaclesKilled);
            Destroy(collision.gameObject);
            //Destroy(explosion, 2);
        }
    }
    

}
