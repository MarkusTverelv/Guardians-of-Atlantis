using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnglerScript2 : MonoBehaviour
{
    [SerializeField] float speed, chaseSpeed, turnSpeed, memory;
    float memoryTimer;
    bool losingTarget;
    [SerializeField] TextMeshProUGUI angelText, memoryText;
    GameObject? target;
    enum state {idle, chasing}
    state currentState = state.idle;
    Rigidbody2D rb;
    int sign;
    void Start()
    {
        memoryTimer = memory;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if(losingTarget)
        {
            memoryTimer -= Time.fixedDeltaTime;
            memoryText.text = string.Format("Memory: {0}", memoryTimer);
            if (memoryTimer <= 0)
            {
                target = null;
                currentState = state.idle;
                memoryTimer = memory;
                losingTarget = false;
            }
        }

        

        float rot = transform.eulerAngles.z;
        if (rot > 280 || rot < 80)
            transform.localScale = new Vector2(transform.localScale.x, Mathf.Abs(transform.localScale.y));
        else if (rot > 100 && rot < 260)
            transform.localScale = new Vector2(transform.localScale.x, -Mathf.Abs(transform.localScale.y));

        float angel = 0;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        Vector2 forward = -transform.right * Mathf.Sign(transform.localScale.x) * 20;
        Vector2 horizontal = Vector3.left * Mathf.Sign(transform.localScale.x) * 20;
        

        //Forward
        Debug.DrawRay(transform.position, forward, Color.green);


        float appliedSpeed = 0;
        switch (currentState)
        {
            case state.idle:

                //Horizontal
                Debug.DrawRay(transform.position, horizontal, Color.red);
                appliedSpeed = speed;
                angel = Vector2.SignedAngle(horizontal, forward);
                break;

            case state.chasing:
                appliedSpeed = chaseSpeed;
                Vector2 targetVector = target.transform.position - transform.position;
                Debug.DrawRay(transform.position, targetVector, Color.red);
                angel = Vector2.SignedAngle(forward, targetVector);
                break;
        }

        appliedSpeed *= Mathf.Sign(transform.localScale.x);

        angelText.text = string.Format("Angel: {0:0.00}", angel);

        if (angel < 0)
            rb.AddTorque(-turnSpeed);
        else if (angel > 0)
            rb.AddTorque(turnSpeed);

        
        rb.AddRelativeForce(Vector2.left* Mathf.Sign(transform.localScale.x) * appliedSpeed);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") && currentState == state.idle)
        {
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && currentState == state.idle)
        {
            currentState = state.chasing;
            target = collision.gameObject;
            Debug.Log(string.Format("{0}: Aquired Target {1}", gameObject, target));
        }
        else if (collision.gameObject == target)
        {
            losingTarget = false;
            memoryTimer = memory;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject == target)
        {
            losingTarget = true;
        }
    }
}
