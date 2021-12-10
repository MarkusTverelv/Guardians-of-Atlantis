using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eel : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] points;
    public float speed;
    int currentPoint = 0;
    Rigidbody2D body;
    CapsuleCollider2D capsule;
    bool turn;

    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 point = points[currentPoint].transform.position;
        if(Vector3.Distance(transform.position, point) >1)
        {

            
            Vector3 norTar = (point - transform.position).normalized;
            float angle = Mathf.Atan2(norTar.y, norTar.x) * Mathf.Rad2Deg;
            Quaternion rotation = new Quaternion();
            rotation.eulerAngles = new Vector3(0, 0, angle + 90);
           
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed/10);

            body.AddRelativeForce(new Vector2(0, -1) * speed);
        }
        else
        {

            if (points.Length == currentPoint + 1&& !turn)
                turn = true;
            else if(0 == currentPoint && turn)
                turn = false;

            if (turn)
                currentPoint--;
            else
                currentPoint++;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinko"))
            collision.gameObject.GetComponent<NewPinkoMovement>().TakeDamage();

        if (collision.gameObject.CompareTag("Yello"))
            collision.gameObject.GetComponent<NewPinkoMovement>().TakeDamage();
    }
}
