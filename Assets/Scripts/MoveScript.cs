using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    Rigidbody2D body;
    [HideInInspector] public float turn;
    [HideInInspector] public float move;
    public float moveSpeed;
    public float turnSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    
    
    private void FixedUpdate()
    {
        
        body.AddTorque(turn * -turnSpeed);
        body.AddRelativeForce(new Vector3(0, move * moveSpeed));
    }
}