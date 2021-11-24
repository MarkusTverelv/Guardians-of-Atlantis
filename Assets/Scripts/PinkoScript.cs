using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkoScript : MonoBehaviour
{
    MoveScript moveScript;
    private void Start()
    {
        moveScript = GetComponent<MoveScript>();
    }
    private void Update()
    {
        moveScript.turn = Input.GetAxis("Horizontal2");
        moveScript.move = Input.GetAxis("Vertical2");
    }
}