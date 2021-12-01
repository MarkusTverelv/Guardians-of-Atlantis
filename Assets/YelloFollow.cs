using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerStates
{
    Idle,
    Moving,
    Dash
}

public class YelloFollow : MonoBehaviour
{
    public float rotateSpeed;
    public float distance;

    private float angle = 0.0f;
    private float direction = 0.0f;

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
        direction = Input.GetAxisRaw("Horizontal");
        angle += direction * rotateSpeed * Time.deltaTime;
        offset = new Vector3(Mathf.Sin(angle) * distance, Mathf.Cos(angle) * distance);

        if (direction != 0)
            currentPlayerState = PlayerStates.Moving;
        else if (Input.GetKeyDown(KeyCode.Return))
            currentPlayerState = PlayerStates.Dash;
        else
            currentPlayerState = PlayerStates.Idle;

        PlayerStateManager(currentPlayerState);
    }
    private void PlayerStateManager(PlayerStates current)
    {
        switch (current)
        {
            case PlayerStates.Idle:
                canMove = false;
                break;
            case PlayerStates.Moving:
                canMove = true;
                break;
            case PlayerStates.Dash:
                canMove = true;
                Dash(parentRigidbody2D.position - thisRigidbody2D.position);
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
    private void Dash(Vector2 direction)
    {
        thisRigidbody2D.AddForce(-direction.normalized * 100, ForceMode2D.Impulse);
    }
}