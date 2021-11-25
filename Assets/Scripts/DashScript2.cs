using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript2 : MonoBehaviour
{
    private PlayerScript player;
    List<Rigidbody2D> line = new List<Rigidbody2D>();
    List<Rigidbody2D> bodies = new List<Rigidbody2D>();
    public int dashForce;
    public float dashCooldown;
    private float dashTimer = 5;
    int doubleTap;
    bool dashReady;


    // Start is called before the first frame update
    void Start()
    {
        bodies.Add(GetComponent<Rigidbody2D>());
        bodies.Add(GameObject.Find("Pinko").GetComponent<Rigidbody2D>());
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
        if(Input.GetKeyDown(KeyCode.UpArrow))
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
        
        bodies[0].AddRelativeForce(Vector2.up * dashForce, ForceMode2D.Impulse);
        bodies[1].AddForce(bodies[0].velocity, ForceMode2D.Impulse);
        foreach(Rigidbody2D body in line)
            body.AddForce(bodies[0].velocity/150, ForceMode2D.Impulse);
    }
}
