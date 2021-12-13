using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NewPinkoMovement : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float deacceleration;
    public float moveSpeed, turnSpeed, maxMoveSpeed;
    public float shootForce;
    bool inv;
    public int currentHealth, maxHealth;

    public Image healthbar;

    private float move, turn;

    private Rigidbody2D rb;
    private Vector2 velocity = Vector2.zero;

    [SerializeField]
    private Transform pinkoGFXTransform;

    public Transform PinkoGFX
    {
        get { return pinkoGFXTransform; }
        set
        {
            pinkoGFXTransform = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;

    }

    private void Update()
    {
        move = Input.GetAxisRaw("Vertical2");
        turn = Input.GetAxisRaw("Horizontal2");
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
        return (pinkoGFXTransform.up * input).normalized;
    }

    public void Turn()
    {
        pinkoGFXTransform.Rotate(Vector3.forward, -turn * turnSpeed * Time.deltaTime);
    }

    public bool Shoot(float dist, Rigidbody2D yello, bool shoot, bool shootPower)
    {
        if (dist >= 1.5f)
            yello.position = Vector2.MoveTowards(yello.position,
                rb.position, 50 * Time.deltaTime);

        else if (dist < 1.5f)
            yello.position = rb.position;

        if (!shoot && !shootPower)
        {
            shootForce += 0.5f;

            if (shootForce > 300.0f)
                shootForce = 300.0f;
        }

        if (shoot)
        {
            yello.tag = "Projectile";
            yello.AddForce(pinkoGFXTransform.up * shootForce, ForceMode2D.Impulse);
            shootForce = 100;
            Invoke("ChangeTag", 2);
            return true;
        }

        return false;
    }
    
    public void TakeDamage()
    {
        if (currentHealth > 1)
            currentHealth--;
        else
            SceneManager.LoadScene("GameOver");
    }
    private void ChangeTag()
    {
        transform.parent.GetChild(1).tag = "Yello";
    }

}
