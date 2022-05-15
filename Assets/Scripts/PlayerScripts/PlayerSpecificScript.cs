using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpecificScript : MonoBehaviour
{
    Vector2 velocity = Vector2.zero;
    float move, turn;
    
    float moveSpeed, turnSpeed, maxMoveSpeed;
    private Rigidbody2D rb;
    
    [Range(0.0f, 1.0f)]
    public float deacceleration;

    bool inv;
    
    [SerializeField]
    public Transform playerTransform;

    PlayerSharedScript altMovement;

    private void Start()
    {
        altMovement = transform.parent.GetComponent<PlayerSharedScript>();
        moveSpeed = altMovement.moveSpeed;
        maxMoveSpeed = altMovement.maxMoveSpeed;
        turnSpeed = altMovement.turnSpeed;

        rb = GetComponent<Rigidbody2D>();
    }

    public void Move()
    {
        velocity += NormalizedInput(move) * moveSpeed * Time.fixedDeltaTime;

        if (velocity.magnitude > maxMoveSpeed)
            velocity = velocity.normalized * maxMoveSpeed;

        else if (NormalizedInput(move) == Vector2.zero)
            velocity *= deacceleration;

        rb.velocity += velocity;
    }

    private void Update()
    {
        if (gameObject.CompareTag("Yello") || gameObject.CompareTag("Projectile"))
        {
            move = Input.GetAxisRaw("Vertical");
            turn = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            move = Input.GetAxisRaw("Vertical2");
            turn = Input.GetAxisRaw("Horizontal2");
        }
    }
    private Vector2 NormalizedInput(float input)
    {
        return (playerTransform.right * -input).normalized;
    }
    public void Turn()
    {
        playerTransform.Rotate(Vector3.forward, -turn * turnSpeed * Time.deltaTime);
    }
    public void Pull(float dist, Rigidbody2D pinko)
    {
        if (dist >= 1.5f)
            pinko.position = Vector2.MoveTowards(pinko.position,
                rb.position, 50 * Time.deltaTime);

        else if (dist < 1.5f)
        {
            pinko.position = rb.position;
        }
    }
   
}
