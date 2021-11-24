using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowScript : MonoBehaviour
{
    MoveScript moveScript;
    private void Start()
    {
        moveScript = GetComponent<MoveScript>();
    }
    private void Update()
    {
        moveScript.turn = Input.GetAxis("Horizontal");
        moveScript.move = Input.GetAxis("Vertical");
    }
}
