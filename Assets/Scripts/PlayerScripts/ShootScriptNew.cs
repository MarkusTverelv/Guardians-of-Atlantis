using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootScriptNew : MonoBehaviour
{
    public float shootForce;
    Rigidbody2D rb;
    Transform playerTransform;
    GameObject yello, yelloHB;
    Image yelloHBImage;
    SpriteRenderer yelloSpriteRenderer;
    Sprite circle;
    private void Start()
    {
        yelloHB = GameObject.Find("YelloHB");
        yelloHBImage = yelloHB.GetComponent<Image>();
        yello = GameObject.Find("Yello");
        yelloSpriteRenderer = yello.GetComponent<SpriteRenderer>();
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
            yelloHBImage.enabled = false;
            shootForce = 100;
            Invoke("ResetYello", 2);
            return true;
        }

        return false;
    }
    private void ResetYello()
    {
        yello.tag = "Yello";
        
    }
}
