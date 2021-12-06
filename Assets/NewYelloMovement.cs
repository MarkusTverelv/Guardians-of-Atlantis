using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewYelloMovement : MonoBehaviour
{
    public float moveSpeed, turnSpeed;

    private float move, turn;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        move = Input.GetAxisRaw("Vertical");
        turn = Input.GetAxisRaw("Horizontal");
    }

    public void Move()
    {
        rb.AddRelativeForce(new Vector2(0, move * moveSpeed));
        rb.AddTorque(turn * -turnSpeed);
    }
}
