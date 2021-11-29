using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobScript : MonoBehaviour
{
    Rigidbody2D body;
    bool turn;
    [SerializeField] float speed, amount;
    Vector3 startPos;
    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        startPos = transform.position;
    }

    // Update is called once per frame
    
    void FixedUpdate()
    {
        if (turn)
        {
            transform.position += new Vector3(0, speed);
            if (transform.position.y > startPos.y + amount)
                turn = false;
        }
        else
        {
            transform.position += new Vector3(0, -speed);
            if (transform.position.y < startPos.y - amount)
                turn = true;
        }
    }
}
