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

    public float dashDuration = 1;

    int doubleTap;
    bool dashReady = true;
    bool isYello;
    bool isDashing;
    KeyCode dashKey;

    // Start is called before the first frame update
    void Start()
    {
        isYello = gameObject.name == "Yello";
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (isYello)
        {
            bodies.Add(GetComponent<Rigidbody2D>());
            bodies.Add(transform.parent.transform.Find("Pinko").GetComponent<Rigidbody2D>());
            dashKey = KeyCode.UpArrow;
        }
        else
        {
            dashKey = KeyCode.W;
            bodies.Add(GetComponent<Rigidbody2D>());
            bodies.Add(transform.parent.transform.Find("Yello").GetComponent<Rigidbody2D>());
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
        if (Input.GetKeyDown(dashKey)&&dashReady)
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

    

    private void Dash()
    {
        //StartCoroutine(DashTimer(dashDuration));
        //StartCoroutine(ApplyDash());
        bodies[0].AddRelativeForce(Vector2.up * dashForce, ForceMode2D.Impulse);
        bodies[1].AddForce(bodies[0].velocity, ForceMode2D.Impulse);

        foreach (Rigidbody2D body in line)
            body.AddForce(bodies[0].velocity / 150, ForceMode2D.Impulse);

        player.MakeInv(1);
    }
    IEnumerator DashTimer(float time)
    {
        isDashing = true;
        yield return new WaitForSeconds(time);
        isDashing = false;
    }
    IEnumerator ApplyDash()
    {
        Vector2 baseVelocity = bodies[0].velocity;
        while(isDashing)
        {
            bodies[0].velocity = baseVelocity * dashForce;
            bodies[1].velocity = bodies[0].velocity;
            yield return new WaitForFixedUpdate();
        }
        bodies[0].velocity /= dashForce;
        bodies[1].velocity /= dashForce;
        foreach (Rigidbody2D body in line)
            body.velocity /= dashForce;
    }
}
