﻿using System.Collections;
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

        if (Input.GetKeyDown(KeyCode.L))
            currentState = NewPlayerStates.Shield;

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
                break;
            case NewPlayerStates.Shield:
                if (yelloMove.Pull(distance, pinkoRigidbody))
                    if (StartCoroutine(yelloMove.Shield(distance, pinkoRigidbody)) == null)
                        currentState = NewPlayerStates.Idle;
                break;
            case NewPlayerStates.Attack:
                if (pinkoMove.Shoot(distance, yelloRigidbody))
                    currentState = NewPlayerStates.Idle;
                break;
            case NewPlayerStates.Push:
                break;
            default:
                break;
        }
    }
}
