using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D body;
    [HideInInspector] public float turn, move;
    [HideInInspector] public int currentHealth;
    public int maxHealth;
    public float moveSpeed, turnSpeed, invTime;
    SpriteRenderer spriteRenderer;
    bool inv, blink;
    void Start()
    {
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
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
    public void hurt()
    {
        if(!inv)
        {
            currentHealth--;
            StartCoroutine(invTimer());
            Debug.Log(gameObject.name + " health: " + currentHealth);
        }
    }
    
}