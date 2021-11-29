using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript2 : MonoBehaviour
{
    public int dashForce;
    public float dashCooldown;

    SpriteRenderer spriteRenderer;

    List<Rigidbody2D> line = new List<Rigidbody2D>();
    List<Rigidbody2D> bodies = new List<Rigidbody2D>();

    private PlayerScript player;

    float dashTime = 1;

    int doubleTap;
    bool dashReady;
    bool isYello;

    KeyCode dashKey;

    // Start is called before the first frame update
    void Start()
    {
        isYello = gameObject.name == "Yello";
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (isYello)
        {
            bodies.Add(GetComponent<Rigidbody2D>());
            bodies.Add(GameObject.Find("Pinko").GetComponent<Rigidbody2D>());
            dashKey = KeyCode.UpArrow;
        }
        else
        {
            dashKey = KeyCode.W;
            bodies.Add(GetComponent<Rigidbody2D>());
            bodies.Add(GameObject.Find("Yello").GetComponent<Rigidbody2D>());
        }

        player = GetComponent<PlayerScript>();

        foreach (Transform child in GameObject.Find("Line").transform)
            line.Add(child.gameObject.GetComponent<Rigidbody2D>());
    }

    IEnumerator doubleTapReset(float time)
    {
        //Debug.Log("Doubletap window open");
        yield return new WaitForSeconds(time);
        //Debug.Log("Doubletap window close");
        doubleTap = 0;
    }

    IEnumerator DashCooldown()
    {
        dashReady = false;
        yield return new WaitForSeconds(dashCooldown);
        dashReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(dashKey))
        {
            if (doubleTap > 0)
            {
                Debug.Log("tap 2");
                Dash();
                StartCoroutine(DashCooldown());
                doubleTap = 0;
            }
            else
            {
                doubleTap++;
                Debug.Log("tap 1");
                StartCoroutine(doubleTapReset(0.5f));
            }
        }
    }

    private void FixedUpdate()
    {
        float f = 255 / dashCooldown;
    }

    private void Dash()
    {
        if (!dashReady)
            return;

        bodies[0].AddRelativeForce(Vector2.up * dashForce, ForceMode2D.Impulse);
        bodies[1].AddForce(bodies[0].velocity, ForceMode2D.Impulse);

        foreach (Rigidbody2D body in line)
            body.AddForce(bodies[0].velocity / 150, ForceMode2D.Impulse);

        player.MakeInv(1);
    }
}
