using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPinkoMovement : MonoBehaviour
{
    public float moveSpeed, turnSpeed, maxMoveSpeed;

    private float move, turn;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;

    [SerializeField]
    private Transform pinkoGFXTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        move = Input.GetAxisRaw("Vertical2");
        turn = Input.GetAxisRaw("Horizontal2");
    }

    public void Move()
    {
        velocity += NormalizedInput(move) * moveSpeed * Time.fixedDeltaTime;

        if (velocity.magnitude > maxMoveSpeed)
            velocity = velocity.normalized * maxMoveSpeed;

        else if (NormalizedInput(move) == Vector2.zero)
            velocity *= 0.95f;

        rb.velocity += velocity;
    }

    private Vector2 NormalizedInput(float input)
    {
        return (pinkoGFXTransform.up * input).normalized;
    }

    public void Turn()
    {
        pinkoGFXTransform.Rotate(Vector3.forward, -turn * turnSpeed * Time.deltaTime);
    }
}
