using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkoScript : MonoBehaviour
{
    PlayerScript moveScript;
    private void Start()
    {
        moveScript = GetComponent<PlayerScript>();
    }
    private void Update()
    {
        moveScript.turn = Input.GetAxis("Horizontal2");
        moveScript.move = Input.GetAxis("Vertical2");
    }
}
