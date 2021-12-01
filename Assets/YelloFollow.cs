using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    Idle,
    Moving
}

public class YelloFollow : MonoBehaviour
{
    public float rotateSpeed;
    public float distance;

    private Rigidbody2D thisRigidbody2D;
    private Rigidbody2D parentRigidbody2D;

    PlayerStates currentPlayerState = PlayerStates.Idle;

    private Vector3 offset;
    private bool canMove = false;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        parentRigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction = Input.GetAxisRaw("Horizontal");
        float angle = 0.0f;

        angle += direction * rotateSpeed * Time.deltaTime;
        offset = new Vector3(Mathf.Sin(angle) * distance, Mathf.Cos(angle) * distance);

        if (direction != 0)
            currentPlayerState = PlayerStates.Moving;
        else
            currentPlayerState = PlayerStates.Idle;

        switch (currentPlayerState)
        {
            case PlayerStates.Idle:
                canMove = false;
                break;
            case PlayerStates.Moving:
                canMove = true;
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
            Move(offset);
    }

    private void Move(Vector3 rotation)
    {
        thisRigidbody2D.position = parentRigidbody2D.position + (Vector2)rotation;
    }
}
