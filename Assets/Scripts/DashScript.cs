using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScript : MonoBehaviour
{
    private PlayerScript player;
    public Rigidbody2D rb;
    public int dashForce;
    public float dashRate;
    private float dashTimer = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {

        dashTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.L) && dashTimer >= dashRate)
        {

            Dash();
            dashTimer = 0;
        }
    }

    private void Dash()
    {
        
        rb.AddRelativeForce(new Vector3(0, player.move, 0) * dashForce);

        
    }
}
