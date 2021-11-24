using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowScript : MonoBehaviour
{
    Rigidbody2D body;
    float turn;
    float move;
    public float speed;
    public float turnSpeed;
    [HideInInspector] public bool hold;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        turn = Input.GetAxis("Horizontal2");
        move = Input.GetAxis("Vertical2");

    }
    private void FixedUpdate()
    {
        if (hold)
            body.constraints = RigidbodyConstraints2D.FreezeAll;
        else
            body.constraints = RigidbodyConstraints2D.None;
        body.AddTorque(turn*turnSpeed);
        body.AddRelativeForce(new Vector3 (0, move * speed));
    }
}
