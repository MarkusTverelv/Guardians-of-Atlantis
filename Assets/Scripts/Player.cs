using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int maxHealth = 5;
    [HideInInspector] public int currentHealth;
    Vector3 movement;
    Rigidbody2D body;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();
    }
    private void FixedUpdate()
    {
        body.AddForce(movement * speed);
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Damaging"))
        {
            currentHealth--;
            Debug.Log("health: " + currentHealth);
        }
    }
}
