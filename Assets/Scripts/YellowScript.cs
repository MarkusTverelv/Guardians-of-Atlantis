using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowScript : MonoBehaviour
{
    PlayerScript moveScript;
    private void Start()
    {
        transform.position += new Vector3(0, 0, -1);
        moveScript = GetComponent<PlayerScript>();
    }
    private void Update()
    {
        if (this.GetComponent<SpriteRenderer>().enabled)
        {
            moveScript.turn = Input.GetAxis("Horizontal");
            moveScript.move = Input.GetAxis("Vertical");
        }
    }
}
