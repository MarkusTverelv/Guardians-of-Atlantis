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

    Vector3 midPoint;

    float distance = 0.0f;
    bool shoot = false;

    NewPlayerStates currentState = NewPlayerStates.Moving;

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
        if (Input.GetKeyDown(KeyCode.R))
            currentState = NewPlayerStates.Attack;

        if (Input.GetKey(KeyCode.Return))
            shoot = false;

        if (Input.GetKeyUp(KeyCode.Return))
            shoot = true;

        if (Input.GetKeyDown(KeyCode.L))
            currentState = NewPlayerStates.Shield;

        yelloMove.Turn();
        pinkoMove.Turn();
    }

    //M=(2x1​+x2​​,2y1​+y2​​)

    private void FixedUpdate()
    {
        distance = (yelloRigidbody.position - pinkoRigidbody.position).magnitude;
        midPoint = (yelloRigidbody.position + pinkoRigidbody.position) / 2;

        StateSwitcher();
    }

    private void Attraction()
    {
        if (distance > 2.0f)
        {
            yelloRigidbody.position = Vector3.Slerp(yelloRigidbody.position, midPoint, (distance / 10) * Time.deltaTime);
            pinkoRigidbody.position = Vector3.Slerp(pinkoRigidbody.position, midPoint, (distance / 10) * Time.deltaTime);
        }
        else
            return;
    }

    private void StateSwitcher()
    {
        switch (currentState)
        {
            case NewPlayerStates.Idle:
                Attraction();
                break;
            case NewPlayerStates.Moving:
                Attraction();
                yelloMove.Move();
                pinkoMove.Move();
                break;
            case NewPlayerStates.Shield:
                yelloMove.Move();
                break;
            case NewPlayerStates.Attack:
                pinkoMove.Move();
                if (pinkoMove.Shoot(distance, yelloRigidbody, shoot))
                    currentState = NewPlayerStates.Moving;
                break;
            case NewPlayerStates.Push:
                break;
            default:
                break;
        }
    }
}
