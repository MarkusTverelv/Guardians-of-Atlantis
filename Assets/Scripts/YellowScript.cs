using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowScript : MonoBehaviour
{
    PlayerScript moveScript;
    private void Start()
    {
        moveScript = GetComponent<PlayerScript>();
    }
    private void Update()
    {
        moveScript.turn = Input.GetAxis("Horizontal");
        moveScript.move = Input.GetAxis("Vertical");
    }
}
