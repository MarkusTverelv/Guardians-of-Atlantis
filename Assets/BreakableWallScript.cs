using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallScript : MonoBehaviour
{
    BoxCollider2D boxCollider2D;
    public float fallDownTime = 1;
    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Projectile"))
        {
            boxCollider2D.enabled = false;
        }
    }
    IEnumerator FallDown ()
    {

    }
}
