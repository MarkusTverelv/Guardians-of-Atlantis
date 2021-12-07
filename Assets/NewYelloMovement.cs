using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewYelloMovement : MonoBehaviour
{
    public GameObject shieldPrefab;
    public float moveSpeed, turnSpeed, maxMoveSpeed;

    [Range(0.0f, 1.0f)]
    public float deacceleration;

    private float move, turn;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;

    [SerializeField]
    private Transform yelloGFXTransform;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        move = Input.GetAxisRaw("Vertical");
        turn = Input.GetAxisRaw("Horizontal");
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

    private Vector2 NormalizedInput(float input)
    {
        return (yelloGFXTransform.up * input).normalized;
    }

    public void Turn()
    {
        yelloGFXTransform.Rotate(Vector3.forward, -turn * turnSpeed * Time.deltaTime);
    }

    public bool Shield(float dist, Rigidbody2D pinko)
    {
        bool deployShield = false;

        if (dist >= 1.5f)
            pinko.position = Vector2.MoveTowards(pinko.position,
                rb.position, 50 * Time.deltaTime);

        else if (dist < 1.5f)
            pinko.position = rb.position;

        if (pinko.position == rb.position)
        {
            deployShield = true;

            if (deployShield)
            {
                GameObject shield = Instantiate(shieldPrefab, rb.position, Quaternion.identity);
                Destroy(shield, 2);

                if (shield != null)
                    deployShield = false;
                else
                    return true;
            }
        }

        return false;
    }
}
