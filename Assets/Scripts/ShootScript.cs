using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class ShootScript : MonoBehaviour
{
    GameObject yello, line, pinko, arrowDirection, jellowProjectile2;
    [SerializeField] GameObject jellowProjectile;
    SpriteRenderer arrowSpriteRenderer;
    bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        arrowDirection = GameObject.Find("ArrowAim");
        arrowSpriteRenderer = arrowDirection.GetComponent<SpriteRenderer>();
        yello = GameObject.Find("Yello");
        pinko = GameObject.Find("Pinko");
        line = GameObject.Find("Line");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && yello.GetComponent<CircleCollider2D>().enabled && pinko.GetComponent<SpriteRenderer>().enabled)
            StartCoroutine(AttachTimer());

        if (Input.GetKeyDown(KeyCode.Return) && canShoot)
        {
            jellowProjectile2.GetComponent<JellowProjectileScript>().currentState = JellowProjectileScript.ProjectileState.fire;
            canShoot = false;
            Invoke("ActivateCollider", 0.25f);
        }
    }
    IEnumerator AttachTimer()
    {
        GetComponent<PlayerScript>().TurnOffOnComponents(yello, line, false);
        jellowProjectile2 = Instantiate(jellowProjectile, yello.transform.position, Quaternion.identity);
        GameObject.FindGameObjectWithTag("YelloHB").GetComponent<Image>().enabled = false;
        yield return new WaitForSeconds(2);
        jellowProjectile2.GetComponent<JellowProjectileScript>().currentState = JellowProjectileScript.ProjectileState.idle;
        canShoot = true;
        arrowSpriteRenderer.enabled = true;

    }

    private void ActivateCollider()
    {
        jellowProjectile2.GetComponent<CircleCollider2D>().enabled = true;
    }

}
