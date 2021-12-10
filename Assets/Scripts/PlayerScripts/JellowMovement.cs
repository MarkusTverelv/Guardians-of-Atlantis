using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellowMovement : MonoBehaviour
{
    public float speed;
    public float maxSpeed;
    private Vector2 velocity;
    private Rigidbody2D rb;
    public float dashForce;
    private bool canDash;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.L))
        {
            canDash = true;
            
        }

    }

    private void FixedUpdate()
    {
        if (canDash)
        {
            Dash();
            canDash = false;
        }
    }


    Vector2 NormalizedDirectionalMovement()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void Move()
    {
        velocity += NormalizedDirectionalMovement() * speed * Time.fixedDeltaTime;

        if (velocity.magnitude > maxSpeed)
           velocity = velocity.normalized * maxSpeed;

        if (NormalizedDirectionalMovement() == Vector2.zero)
            velocity *= 0.94f;

        rb.velocity = velocity;
    }
    private void Dash()
    {
        rb.AddForce(NormalizedDirectionalMovement() * dashForce, ForceMode2D.Impulse);
    }
}
