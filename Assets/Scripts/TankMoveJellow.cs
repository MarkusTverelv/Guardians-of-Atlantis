using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMoveJellow : MonoBehaviour
{
    Rigidbody2D body;
    float turn;
    float move;
    public float speed;
    public float turnSpeed;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        turn = Input.GetAxis("Horizontal");
        move = Input.GetAxis("Vertical");

    }
    private void FixedUpdate()
    {
        body.AddTorque(turn * -turnSpeed);
        body.AddRelativeForce(new Vector3(0, move * speed));
    }
}