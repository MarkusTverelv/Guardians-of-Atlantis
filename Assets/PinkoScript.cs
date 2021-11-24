using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkoScript : MonoBehaviour
{
    Rigidbody2D body;
    Vector3 movement;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed;
    }
    private void FixedUpdate()
    {
        body.AddForce(movement);
    }
}
