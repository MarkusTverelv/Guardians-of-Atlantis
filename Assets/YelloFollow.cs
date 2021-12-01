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
    public float dashForce;

    private float angle = 0.0f;
    private float direction = 0.0f;

    private bool canMove = false;

    private Rigidbody2D thisRigidbody2D;
    private Rigidbody2D parentRigidbody2D;

    private Vector3 offset;

    PlayerStates currentPlayerState = PlayerStates.Idle;

    // Start is called before the first frame update
    void Start()
    {
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        parentRigidbody2D = transform.parent.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();

        #region PlayerState checks
        if (direction != 0)
            currentPlayerState = PlayerStates.Moving;
        else if (Input.GetKeyDown(KeyCode.Return))
            currentPlayerState = PlayerStates.Dash;
        else
            currentPlayerState = PlayerStates.Idle;
        #endregion

        PlayerStateManager(currentPlayerState);
    }
    private void RotatePlayer()
    {
        direction = Input.GetAxisRaw("Horizontal");

        angle += direction * rotateSpeed * Time.deltaTime;
        offset = new Vector3(Mathf.Sin(angle) * distance, Mathf.Cos(angle) * distance);
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
                Dash(thisRigidbody2D.position - parentRigidbody2D.position);
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
        thisRigidbody2D.position = Vector2.MoveTowards(thisRigidbody2D.position, parentRigidbody2D.position + (Vector2)rotation, 30 * Time.deltaTime);
    }

    private void Dash(Vector2 direction)
    {
        thisRigidbody2D.AddForce(direction.normalized * dashForce, ForceMode2D.Impulse);
    }
}