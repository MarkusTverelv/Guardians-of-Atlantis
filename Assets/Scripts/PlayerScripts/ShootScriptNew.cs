using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScriptNew : MonoBehaviour
{
    public float shootForce;
    Rigidbody2D rb;
    Transform playerTransform;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GetComponent<PlayerSpecificScript>().playerTransform;
    }
    public bool Shoot(float dist, Rigidbody2D yello, bool shoot, bool shootPower)
    {
        if (dist >= 1.5f)
            yello.position = Vector2.MoveTowards(yello.position, rb.position, 50 * Time.deltaTime);

        else if (dist < 1.5f)
            yello.position = rb.position;

        if (!shoot && !shootPower)
        {
            shootForce += 0.5f;

            if (shootForce > 300.0f)
                shootForce = 300.0f;
        }

        if (shoot)
        {
            yello.tag = "Projectile";
            yello.AddForce(playerTransform.up * shootForce, ForceMode2D.Impulse);
            shootForce = 100;
            Invoke("ChangeTag", 2);
            return true;
        }

        return false;
    }
}
