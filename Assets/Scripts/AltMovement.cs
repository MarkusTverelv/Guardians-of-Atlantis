using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltMovement : MonoBehaviour
{
    public float turnSpeed;
    public float moveSpeed;

    Rigidbody2D rb;

    float move;
    float turn;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
         move = Input.GetAxis("Vertical2");
         turn = Input.GetAxis("Horizontal2");
    }

    private void FixedUpdate()
    {
        rb.AddRelativeForce(new Vector2(0, move * moveSpeed));
        rb.AddTorque(turn * -turnSpeed);
    }
}
