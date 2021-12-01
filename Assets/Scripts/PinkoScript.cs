using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class PinkoScript : MonoBehaviour
{
    Light2D light;
    PlayerScript moveScript;
    private void Start()
    {
        transform.position += new Vector3(0, 0, -1);
        moveScript = GetComponent<PlayerScript>();
    }
    private void Update()
    {
        moveScript.turn = Input.GetAxis("Horizontal2");
        moveScript.move = Input.GetAxis("Vertical2");
    }
}
