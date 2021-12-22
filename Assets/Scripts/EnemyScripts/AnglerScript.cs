using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnglerScript : MonoBehaviour
{
    enum State {idle, chasing};
    State _currentState = State.idle;
    State currentState
    {
        get { return _currentState; }
        set 
        {
            _currentState = value;
            Debug.Log("State " + currentState);
        }
    }
    GameObject? target;
    GameObject point;
    int currentPoint = 0;
    [SerializeField] float speed = 1;
    [SerializeField] float chargeSpeed = 10;
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] float targetMemory = 1;
    [SerializeField] Sprite cirle;
    bool lostTarget;
    float lostTargetTimer;
    TextMeshProUGUI targetText, angleText, memoryText;
    Transform endPoint;
    List<Vector3> turnPoints = new List<Vector3>();
    Rigidbody2D rb;

    bool flipTimerActive;
    void Start()
    {
        
        point = Instantiate(new GameObject(), new Vector2(1, 0), Quaternion.identity, transform);
        SpriteRenderer s = point.AddComponent(typeof(SpriteRenderer)) as SpriteRenderer;
        s.sprite = cirle;
        endPoint = transform.parent.Find("EndPoint").GetComponent<Transform>();
        targetText = GameObject.Find("Target Text").GetComponent<TextMeshProUGUI>();
        angleText = GameObject.Find("Angle Text").GetComponent<TextMeshProUGUI>();
        memoryText = GameObject.Find("Memory Text").GetComponent<TextMeshProUGUI>();
        turnPoints.Add(transform.position);
        turnPoints.Add(endPoint.position);
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("State " + currentState);
    }

    private void FixedUpdate()
    {
        //Debug.Log(rb.velocity.x);
        if (lostTarget)
        {
            memoryText.text = string.Format("Memory Timer: {0:0.00}", targetMemory - lostTargetTimer);
            if (lostTargetTimer < targetMemory)
                lostTargetTimer += Time.fixedDeltaTime;
            else
            {
                memoryText.text = "Memory Timer: n/a ";
                lostTargetTimer = 0;
                lostTarget = false;
                target = null;
                currentState = State.idle;
            }
        }
        float appliedSpeed = speed;
        Vector3 forward = -transform.right;
        if (transform.localScale.x < 0)
        {
            appliedSpeed *= -1;
            forward *= -1;
        }
        float rot = transform.eulerAngles.z;
        if(rot > 280 || rot < 80)
        {;
            transform.localScale = new Vector3(transform.localScale.x, Mathf.Abs(transform.localScale.y));
        }
        else if (rot > 100 && rot < 260)
        {
            transform.localScale = new Vector3(transform.localScale.x, -Mathf.Abs(transform.localScale.y));
        }
        
        float angle = 0;
        switch (currentState)
        {
            case State.idle:
            {
                
                //int i = currentPoint % turnPoints.Count;
                Debug.DrawRay(transform.position, forward * 30, Color.red);
                //Debug.DrawLine(transform.position, turnPoints[i], Color.green);
                angle = Vector2.SignedAngle(Vector2.right * Mathf.Sign(forward.x), forward);
                if (angle < 0)
                    rb.AddTorque(rotationSpeed);
                else
                    rb.AddTorque(-rotationSpeed);

                break;
            }

            case State.chasing:
            {
                appliedSpeed *= 2;
                angle = Vector2.SignedAngle(target.transform.position - transform.position, forward);
                angleText.text = string.Format("Angle: {0:0}", angle);
                if (angle < 0)
                    rb.AddTorque(rotationSpeed);
                else
                    rb.AddTorque(-rotationSpeed);
                Debug.DrawLine(transform.position, target.transform.position, Color.green);
                Debug.DrawRay(transform.position, forward * 30, Color.red);
                Debug.Log(Physics2D.Raycast(transform.position, forward * 30).rigidbody.gameObject);
                    
                    
                break;
            }
            
        }
        if (Mathf.Abs(rb.velocity.x) == 0)
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y);
        
        rb.AddForce(forward* appliedSpeed);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (currentState == State.idle && collision.gameObject.CompareTag("Player"))
        {
            target = collision.gameObject;
            targetText.text = "Target: " + target.name;
            currentState = State.chasing;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target == collision.gameObject && collision.gameObject.CompareTag("Player"))
            lostTarget = true;           
    }
    
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (currentState == State.idle && collision.gameObject.CompareTag("Wall"))
    //        transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);

    //}
    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Wall") && currentState == State.charging)
    //        currentState = State.idle;
    //}
}
