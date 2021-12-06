using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltMovement : MonoBehaviour
{
    NewPinkoMovement pinkoMove;
    NewYelloMovement yelloMove;

    // Start is called before the first frame update
    void Start()
    {
        pinkoMove = transform.Find("Pinko").GetComponent<NewPinkoMovement>();
        yelloMove = transform.Find("Yello").GetComponent<NewYelloMovement>();
    }

    private void FixedUpdate()
    {
        pinkoMove.Move();
        yelloMove.Move();
    }
    
}
