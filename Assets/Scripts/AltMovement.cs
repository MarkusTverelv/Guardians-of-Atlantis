using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum NewPlayerStates
{
    Idle,
    Moving,
    Shield,
    Attack,
    Push
}

public class AltMovement : MonoBehaviour
{
    public GameObject pinko;
    public GameObject yello;

    NewPinkoMovement pinkoMove;
    NewYelloMovement yelloMove;

    Rigidbody2D yelloRigidbody;
    Rigidbody2D pinkoRigidbody;

    private float distance = 0.0f;
    private Vector3 midPoint;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(yello.GetComponent<Collider2D>(), pinko.GetComponent<Collider2D>());

        pinkoMove = pinko.GetComponent<NewPinkoMovement>();
        yelloMove = yello.GetComponent<NewYelloMovement>();

        yelloRigidbody = yello.GetComponent<Rigidbody2D>();
        pinkoRigidbody = pinko.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        yelloMove.Turn();
        pinkoMove.Turn();
    }

    //M=(2x1​+x2​​,2y1​+y2​​)

    private void FixedUpdate()
    {
        distance = (yelloRigidbody.position - pinkoRigidbody.position).magnitude;
        midPoint = (yelloRigidbody.position + pinkoRigidbody.position) / 2;

        Attraction();

        yelloMove.Move();
        pinkoMove.Move();
    }

    private void Attraction()
    {
        yelloRigidbody.position = Vector3.Slerp(yelloRigidbody.position, midPoint, (distance / 10) * Time.deltaTime);
        pinkoRigidbody.position = Vector3.Slerp(pinkoRigidbody.position, midPoint, (distance / 10) * Time.deltaTime);
    }
}
