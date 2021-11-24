using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishScript : MonoBehaviour
{
    Rigidbody2D body;
    bool aggro, turn, patrolling;
    CircleCollider2D aggroRange;
    public float speed;
    GameObject player;
    
    Vector3 startPos;
    enum state 
    {
        aggro, 
        returning, 
        patrolling 
    }
    state currentState = state.patrolling;


    // Start is called before the first frame update
    void Start()
    {
        
        startPos = transform.position;
        
        body = GetComponent<Rigidbody2D>();
        aggroRange = GetComponentInChildren<CircleCollider2D>();
    }


    private void FixedUpdate()
    {
        if(currentState == state.aggro)
        {
            body.AddForce((player.transform.position - transform.position).normalized * speed);
        }
        else 
        {
            if(Vector3.Distance(startPos , transform.position)>1 && currentState == state.returning)
            {
                body.AddForce((startPos - transform.position).normalized * speed);
            }
            else 
            {
                currentState = state.patrolling;
                if(turn)
                {
                    body.AddForce(new Vector3(0, 0.5f * speed));
                    if (transform.position.y > startPos.y + 2)
                        turn = false;
                }
                else
                {
                    body.AddForce(new Vector3(0, -0.5f * speed));
                    if (transform.position.y < startPos.y - 2)
                    {
                        turn = true;
                        Debug.Log(turn);
                    }
                        
                    
                }
            }
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            currentState = state.aggro;
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentState = state.returning;
        }
    }

}
