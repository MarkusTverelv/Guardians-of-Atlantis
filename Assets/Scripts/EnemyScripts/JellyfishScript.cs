using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JellyFishType
{
    Blue,
    Red,
    Green
}

public class JellyfishScript : MonoBehaviour
{
    public AudioClip electricity, hurt, death;
    public float speed, agrroRange;
    public ParticleSystem blueExplosion, redExplosion, greenExplosion;

    int health = 2;
    int currentHealth;

    bool aggro, turn, patrolling;
    bool inv;

    Vector3 startPos;

    AudioSource audioSource;

    GameObject player;
    Rigidbody2D body;
    CircleCollider2D aggroCollider;

    SpriteRenderer spriteRenderer;

    enum state
    {
        aggro,
        returning,
        patrolling
    }

    state currentState = state.patrolling;
    public JellyFishType myType;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        currentHealth = health;

        body = GetComponent<Rigidbody2D>();
        spriteRenderer = transform.Find("Shell").GetComponent<SpriteRenderer>();
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();

        transform.Find("Aggro").GetComponent<CircleCollider2D>().radius = agrroRange;
        aggroCollider = transform.Find("Aggro").GetComponentInChildren<CircleCollider2D>();

        aggroCollider.radius = agrroRange;
    }


    private void FixedUpdate()
    {
        if (currentState == state.aggro)
            body.AddForce((player.transform.position - transform.position).normalized * speed);
        else
        {
            if (Vector3.Distance(startPos, transform.position) > 1 && currentState == state.returning)
                body.AddForce((startPos - transform.position).normalized * speed);
            else
            {
                currentState = state.patrolling;
                if (turn)
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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
        {
            currentState = state.aggro;
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
        {
            currentState = state.returning;
        }
    }
    IEnumerator flashRedTimer()
    {
        inv = true;
        spriteRenderer.color = new Color(255, 255, 255, 0);

        yield return new WaitForSeconds(.5f);

        spriteRenderer.color = Color.white;
        inv = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
        {
            collision.gameObject.transform.parent.GetComponent<PlayerSharedScript>().TakeDamage();
        }

        if (collision.gameObject.CompareTag("Projectile") && !inv)
        {
            if (currentHealth > 1)
            {
                StartCoroutine(flashRedTimer());
                currentHealth--;
                audioSource.pitch = 1;
                audioSource.PlayOneShot(hurt);
            }
            else
            {
                audioSource.pitch = 1;
                audioSource.PlayOneShot(death);
                
                if (myType == JellyFishType.Blue)
                    DetonateExplosion(blueExplosion);

                else if (myType == JellyFishType.Red)
                    DetonateExplosion(redExplosion);

                else if (myType == JellyFishType.Green)
                    DetonateExplosion(greenExplosion);

                Destroy(gameObject, .125f);
                audioSource.pitch = 1;

            }
        }

        if (collision.gameObject.CompareTag("Explosion"))
        {
            if (currentHealth > 1)
            {
                StartCoroutine(flashRedTimer());
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

    private void DetonateExplosion(ParticleSystem explosion)
    {
        Instantiate(explosion, spriteRenderer.transform.position, Quaternion.identity);
    }
}
