using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTentacles : MonoBehaviour
{
    public int health;
    public bool imActive;
    float posY;

    private GameObject yello;
    private GameObject pinko;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        yello = GameObject.Find("Yello");
        pinko = GameObject.Find("Pinko");

        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), yello.GetComponent<CircleCollider2D>());
        Physics2D.IgnoreCollision(gameObject.GetComponent<BoxCollider2D>(), pinko.GetComponent<CircleCollider2D>());
        posY = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(imActive)
        {
            MoveUp();
        }

        if(!imActive)
        {
            MoveDown();
        }

        this.transform.position = new Vector2(transform.position.x, posY);

        if(health < 1)
        {
            imActive = false;
            health = 5;
        }
    }

    private void MoveUp()
    {
        if (posY < -14)
            posY += 0.2f;
    }

    private void MoveDown()
    {
        if (posY > -65)
            posY -= 0.2f;
    }

}
