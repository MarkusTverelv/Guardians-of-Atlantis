using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpecificScript : MonoBehaviour
{
    Vector2 velocity = Vector2.zero;
    float move, turn;
    [HideInInspector] public int currentHealth;
    public int maxHealth;
    public static int savedHealth;
    float moveSpeed, turnSpeed, maxMoveSpeed, invTime;
    private Rigidbody2D rb;
    [Range(0.0f, 1.0f)]
    public float deacceleration;
    bool inv;
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public Transform playerTransform;
    PlayerSharedScript altMovement;

    private void Start()
    {
        altMovement = transform.parent.GetComponent<PlayerSharedScript>();
        currentHealth = maxHealth;
        maxHealth = savedHealth;
        moveSpeed = altMovement.moveSpeed;
        maxMoveSpeed = altMovement.maxMoveSpeed;
        invTime = altMovement.invTime;
        turnSpeed = altMovement.turnSpeed;

        rb = GetComponent<Rigidbody2D>();

        if (gameObject.CompareTag("Yello"))
        {
            Debug.Log("Yello Health: " + currentHealth);
        }
        else
        {
            Debug.Log("Pinko Health: " + currentHealth);
        }
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
    IEnumerator InvTimer(float time)
    {
        inv = true;
        yield return new WaitForSeconds(time);
        inv = false;
    }
    IEnumerator DamageFlash(float time)
    {
        for (int i = 0; i < time * 10; i++)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(0.1f);
        }
        spriteRenderer.enabled = true;
    }
    public bool TakeDamage()
    {
        if (!inv)
        {
            if (currentHealth > 1)
            {
                StartCoroutine(InvTimer(invTime));
                StartCoroutine(DamageFlash(invTime));
                currentHealth--;
            }

            else
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        return !inv;
    }
}
