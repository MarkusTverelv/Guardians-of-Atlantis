using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    TrailRenderer trailRenderer;
    Rigidbody2D body;
    [HideInInspector] public float turn, move;
    [HideInInspector] public int currentHealth;
    public int maxHealth;
    public float moveSpeed, turnSpeed, invTime, regenTime;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    public AudioClip hurtClip, healClip;
    public AudioClip[] spalshClips;
    bool blink;
    [HideInInspector] public bool inv;
    int regenCounter;
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        audioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(regenTimer());
        StartCoroutine(Splash());
    }
    private void FixedUpdate()
    {
        //if (body.velocity.magnitude > 1)
        //    trailRenderer.time = 1;
        //else
        //    trailRenderer.time = 0.5f;
        
        body.AddTorque(turn * -turnSpeed);
        body.AddRelativeForce(new Vector3(0, move * moveSpeed));
        if (inv)
        {
            if (!blink)
                StartCoroutine(blinkTimer());
        }
        else
            spriteRenderer.enabled = true;

        
            
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
    IEnumerator blinkTimer() 
    {
        spriteRenderer.enabled = !spriteRenderer.enabled;
        blink = true;
        yield return new WaitForSeconds(0.1f);
        blink = false;
    }
    IEnumerator invTimer()
    {
        inv = true;
        yield return new WaitForSeconds(invTime);
        inv = false;
    }
    public bool hurt()
    {
        if(!inv)
        {
            regenCounter = 0;
            audioSource.PlayOneShot(hurtClip);
            if(currentHealth>0)
                currentHealth--;
            StartCoroutine(invTimer());
            Debug.Log(gameObject.name + " health: " + currentHealth);
        }
        return !inv;
    }
    
}