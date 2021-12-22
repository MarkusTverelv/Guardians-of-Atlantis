using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashScriptNew : MonoBehaviour
{
    Rigidbody2D rb;
    public Transform playerTransform;
    public float dashForce = 150;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Dash(Rigidbody2D pinko)
    {
        rb.AddForce(-playerTransform.right * dashForce, ForceMode2D.Impulse);
        pinko.AddForce(-playerTransform.right * dashForce/2, ForceMode2D.Impulse);
    }
}
