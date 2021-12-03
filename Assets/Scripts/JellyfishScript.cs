using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishScript : MonoBehaviour
{
    int health = 3;
    int currentHealth;
    Rigidbody2D body;
    bool aggro, turn, patrolling;
    CircleCollider2D aggroCollider;
    public float speed, agrroRange;
    GameObject player;
    public AudioClip electricity, hurt, death;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    bool inv;
    
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
        currentHealth = health;
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        startPos = transform.position;
        transform.GetChild(0).GetComponent<CircleCollider2D>().radius = agrroRange;
        body = GetComponent<Rigidbody2D>();
        aggroCollider = GetComponentInChildren<CircleCollider2D>();
        aggroCollider.radius = agrroRange;
    }


    private void FixedUpdate()
    {
        if(currentState == state.aggro)
            body.AddForce((player.transform.position - transform.position).normalized * speed);
        else 
        {
            if(Vector3.Distance(startPos , transform.position)>1 && currentState == state.returning)
                body.AddForce((startPos - transform.position).normalized * speed);
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
                        turn = true;
                }
            }
            
        }
        spriteRenderer.flipX = body.velocity.x < 0;
        

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
    IEnumerator flashRedTimer()
    {
        inv = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(1f);
        spriteRenderer.color = Color.white;
        inv = false;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().hurt();

        }
        else if (collision.gameObject.CompareTag("Projectile")&&!inv)
        { 
            if(currentHealth>1)
            {
                StartCoroutine(flashRedTimer());
                Debug.Log(currentHealth);
                currentHealth--;
                audioSource.pitch = 1;
                audioSource.PlayOneShot(hurt);
            }
            else
            {
                audioSource.pitch = 1;
                audioSource.PlayOneShot(death);
                Destroy(gameObject);
                audioSource.pitch = 1;

            }    
        }
            
            
    }



}
