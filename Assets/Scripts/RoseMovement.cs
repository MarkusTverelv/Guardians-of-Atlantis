using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoseMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    //public float maxSpeed;

    Vector2 velocity = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Move();
    }
    Vector2 NormalizedDirectionalMovement()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2")).normalized;
    }

    private void Move()
    {
        velocity += NormalizedDirectionalMovement() * speed * Time.fixedDeltaTime;

        //if (velocity.magnitude > maxSpeed)
          //  velocity = velocity.normalized * maxSpeed;

        if (NormalizedDirectionalMovement() == Vector2.zero)
            velocity *= 0.94f;

        rb.velocity = velocity;
    }
}
