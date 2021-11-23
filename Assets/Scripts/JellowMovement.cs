using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellowMovement : MonoBehaviour
{
    public Rigidbody2D parentRigidbody;
    public float speed;
    public float maxSpeed;
    private Vector2 velocity;
    private Rigidbody2D rb;
    private bool canMove = false;
    private float dist;
    private float maxDist = 4.2f;

    // Start is called before the first frame update
    void Start()
    {
        //parentRigidbody = transform.parent.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        dist = (parentRigidbody.position - rb.position).magnitude;

        if (canMove)
            Move();

        if (dist <= maxDist)
            canMove = true;

        else if (dist > maxDist)
            canMove = false;

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //maxDist = 15;
            Dash();

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

        else if (NormalizedDirectionalMovement() == Vector2.zero)
            velocity *= 0.94f;

        rb.velocity = velocity;
    }
    private void Dash()
    {
        rb.AddForce(NormalizedDirectionalMovement() * 10 * Time.deltaTime, ForceMode2D.Impulse);

    }
}
