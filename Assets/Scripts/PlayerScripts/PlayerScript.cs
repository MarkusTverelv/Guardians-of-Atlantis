using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

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
    public Image healthbar;
    public int maxHealth;
    bool blink;
    bool blikning;
    float moveSpeed, turnSpeed;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    int regenCounter;
    PlayerSharedScript playerShared;
    GameObject line;
    Light2D light2D;
    CircleCollider2D circle;
    List<SpriteRenderer> lineSegmentSprites = new List<SpriteRenderer>();
    void Start()
    {
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
        line = GameObject.Find("Line");
        light2D = GetComponent<Light2D>();
        circle = GetComponent<CircleCollider2D>();
        foreach (Transform child in line.transform)
            lineSegmentSprites.Add(child.gameObject.GetComponent<SpriteRenderer>());
    }
    private void FixedUpdate()
    {
        body.AddTorque(turn * -turnSpeed);
        if (move < 1)
            move /= 2;
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
    

    public void TurnOffOnComponents(bool b)
    {
        spriteRenderer.enabled = b;
        light2D.enabled = b;
        trailRenderer.enabled = b;
        circle.enabled = b;
        healthbar.enabled = b;
        foreach (SpriteRenderer sprite in lineSegmentSprites)
            sprite.enabled = b;
    }
}