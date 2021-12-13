using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScriptNew : MonoBehaviour
{
    Rigidbody2D rb;
    Transform playerTransform;
    public float dashForce = 150;
    private void Start()
    {
        playerTransform = transform;
        rb = GetComponent<Rigidbody2D>();
    }
    public void Dash(Rigidbody2D pinko)
    {
        rb.AddForce(playerTransform.up * dashForce, ForceMode2D.Impulse);
        pinko.AddForce(playerTransform.up * dashForce/2, ForceMode2D.Impulse);
    }
}
