using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;

public class PlayerScript : MonoBehaviour
{
    TrailRenderer trailRenderer;
    Rigidbody2D body;
    [HideInInspector] public float turn, move;
    [HideInInspector] public int currentHealth;
    [HideInInspector] public bool inv;
    [SerializeField] float invTime, regenTime;
    [SerializeField] AudioClip hurtClip, healClip;
    [SerializeField] AudioClip[] spalshClips;
    public int maxHealth;
    bool blink;
    bool blikning;
    float moveSpeed, turnSpeed;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    int regenCounter;
    PlayerSharedScript playerShared;
    void Start()
    {
        
        //Debug.Log("Movespeed = " + moveSpeed);
        //Debug.Log("turnSpeed = " + turnSpeed);
        playerShared = transform.parent.GetComponent<PlayerSharedScript>();
        trailRenderer = GetComponent<TrailRenderer>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(regenTimer());
        StartCoroutine(Splash());
        turnSpeed = playerShared.turnSpeed;
        moveSpeed = playerShared.moveSpeed;
    }
    private void FixedUpdate()
    {
        //if (body.velocity.magnitude > 1)
        //    trailRenderer.time = 1;
        //else
        //    trailRenderer.time = 0.5f;
        
        body.AddTorque(turn * -turnSpeed);
        body.AddRelativeForce(new Vector3(0, move * moveSpeed));
        

        
            
    }
    IEnumerator Splash() 
    {
        if (turn != 0 || move != 0)
        {
            AudioClip splash = spalshClips[Random.Range(0, spalshClips.Length - 1)];
            float pitch = audioSource.pitch;
            audioSource.pitch += Random.Range(2f, 3);
            audioSource.PlayOneShot(splash, 0.05f);
            audioSource.pitch = pitch;
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForFixedUpdate();
        StartCoroutine(Splash());
            
    }
    IEnumerator regenTimer()
    {
        yield return new WaitForSeconds(regenTime/10);
        regenCounter++;
        if(regenCounter>9 && currentHealth<maxHealth)
        {
            currentHealth++;
            regenCounter = 0;
            audioSource.PlayOneShot(healClip);
        }
        StartCoroutine(regenTimer());
    }
    IEnumerator blinkInterval(int blinktimes) 
    {
        for(int i = 0; i<blinktimes; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
    }
    IEnumerator invTimer(float time)
    {
        inv = true;
        yield return new WaitForSeconds(time);
        inv = false;
    }
    public void MakeInv(float time)
    {
        StartCoroutine(invTimer(time));
    }
    public bool hurt()
    {
        if(!inv)
        {
            StartCoroutine(blinkInterval((int)(invTime * 10)));
            regenCounter = 0;
            audioSource.PlayOneShot(hurtClip);

            if (currentHealth > 1)
                currentHealth--;
            else
                SceneManager.LoadScene("GameOver");

            StartCoroutine(invTimer(invTime));
            Debug.Log(gameObject.name + " health: " + currentHealth);
        }
        return !inv;
    }
    

    public void TurnOffOnComponents(GameObject player, GameObject line, bool onOff)
    {
        player.GetComponent<SpriteRenderer>().enabled = onOff;
        player.GetComponent<Light2D>().enabled = onOff;
        player.GetComponent<TrailRenderer>().enabled = onOff;
        player.GetComponent<CircleCollider2D>().enabled = onOff;
        line.GetComponent<LineRenderer>().enabled = onOff;
    }
}