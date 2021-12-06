using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltMovement : MonoBehaviour
{
    public GameObject pinko;
    public GameObject yello;

    NewPinkoMovement pinkoMove;
    NewYelloMovement yelloMove;

    Rigidbody2D yelloRigidbody;
    Rigidbody2D pinkoRigidbody;

    private float distance = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreCollision(yello.GetComponent<Collider2D>(), pinko.GetComponent<Collider2D>());

        pinkoMove = pinko.GetComponent<NewPinkoMovement>();
        yelloMove = yello.GetComponent<NewYelloMovement>();

        yelloRigidbody = yello.GetComponent<Rigidbody2D>();
        pinkoRigidbody = pinko.GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        distance = (yelloRigidbody.position - pinkoRigidbody.position).magnitude;

        yelloRigidbody.position = Vector3.Slerp(yelloRigidbody.position, this.transform.position, (distance / 10) * Time.deltaTime);
        pinkoRigidbody.position = Vector3.Slerp(pinkoRigidbody.position, this.transform.position, (distance / 10) * Time.deltaTime);

        pinkoMove.Move();
        pinkoMove.Turn();

        yelloMove.Move();
        yelloMove.Turn();
    }

}
