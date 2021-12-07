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
    float shootForce = 50.0f;

    bool shoot = false;

    NewPlayerStates currentState = NewPlayerStates.Idle;

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

        yelloMove.Turn();
        pinkoMove.Turn();

        StateSwitcher();
    }

    //M=(2x1​+x2​​,2y1​+y2​​)

    private void FixedUpdate()
    {
        distance = (yelloRigidbody.position - pinkoRigidbody.position).magnitude;
        midPoint = (yelloRigidbody.position + pinkoRigidbody.position) / 2;

        yelloMove.Move();
        pinkoMove.Move();

    }

    private void Attraction()
    {
        yelloRigidbody.position = Vector3.Slerp(yelloRigidbody.position, midPoint, (distance / 10) * Time.deltaTime);
        pinkoRigidbody.position = Vector3.Slerp(pinkoRigidbody.position, midPoint, (distance / 10) * Time.deltaTime);
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
                break;
            case NewPlayerStates.Shield:
                break;
            case NewPlayerStates.Attack:
                Shoot(shoot);
                break;
            case NewPlayerStates.Push:
                break;
            default:
                break;
        }
    }

    private void Shoot(bool canShoot)
    {
        if (distance >= 1.5f)
            yelloRigidbody.position = Vector2.MoveTowards(yelloRigidbody.position, pinkoRigidbody.position, 50 * Time.deltaTime);

        else if (distance < 1.5f)
        {
            if (!canShoot)
                yelloRigidbody.position = pinkoRigidbody.position;
        }

        if (Input.GetKey(KeyCode.Return))
        {
            shootForce += 0.2f;

            if (shootForce > 300.0f)
                shootForce = 300.0f;
        }

        if (Input.GetKeyUp(KeyCode.Return))
        {
            yelloRigidbody.AddForce(pinkoMove.PinkoGFX.up * shootForce, ForceMode2D.Impulse);
            shootForce = 50.0f;
            currentState = NewPlayerStates.Idle;
        }
    }
}
