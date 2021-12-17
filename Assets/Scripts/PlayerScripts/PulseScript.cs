using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseScript : MonoBehaviour
{
    List<GameObject> enemyList;
    public Transform pulseTransform;
    private float range;
    private float rangeMax;
    private bool canGrow = false;
    float pulseTimer;
    public float pulseRate;

    // Start is called before the first frame update
    void Start()
    {
        enemyList = new List<GameObject>();
        rangeMax = 4f;
    }

    // Update is called once per frame
    void Update()
    {
        pulseTimer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q) && pulseTimer >= pulseRate)
        {
            canGrow = true;
            pulseTimer = 0;
        }

        if (canGrow)
        {
            float rangeSpeed = 8f;
            range += rangeSpeed * Time.deltaTime;
            if (range > rangeMax)
            {
                range = rangeMax;
                Invoke("pulseEffect", 0.1f);
                canGrow = false;
            }


        }

        pulseTransform.localScale = new Vector3(range, range);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            addForce(other);
        }

        if (other.gameObject.CompareTag("Bomb"))
        {
            addForce(other);
            other.GetComponent<BombFollowScript>().imActivated = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            Destroy(other.gameObject, 6);
        }
    }

    private void addForce(Collider2D collision)
    {
        for (int i = 0; i < 10; i++)
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Vector2 position = collision.transform.position - this.transform.position;
            rb.AddForce(position, ForceMode2D.Impulse);
        }
    }

    private void pulseEffect()
    {
        range = 0;
    }


}
