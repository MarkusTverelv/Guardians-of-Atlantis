using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellowMovement : MonoBehaviour
{
    public Transform parentTransform;
    public float speed;
    public float maxSpeed;
    private Vector2 velocity;
    private Rigidbody2D rb;
    private Vector2 dist;

    // Start is called before the first frame update
    void Start()
    {
        //parentRigidbody = transform.parent.GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        dist = parentTransform.position - transform.position;

        if (dist.magnitude <= 5)
        {
            Move();
        }
        else if (dist.magnitude > 5)
        {
            transform.RotateAround(parentTransform.position, parentTransform.forward, 50 * Time.deltaTime);
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
}
