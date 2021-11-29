using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;

public class ShootScript : MonoBehaviour
{
    GameObject yello, line, arrowDirection, jellowProjectile2;
    Image yelloHB;
    [SerializeField] GameObject jellowProjectile;
    SpriteRenderer yelloSpriteRenderer, arrowSpriteRenderer;
    Light2D yelloLs;
    TrailRenderer yelloTr;
    PolygonCollider2D yelloCollider;
    LineRenderer lineRenderer;
    bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        arrowDirection = GameObject.Find("ArrowAim");
        arrowSpriteRenderer = arrowDirection.GetComponent<SpriteRenderer>();
        yello = GameObject.Find("Yello");
        yelloCollider = yello.GetComponent<PolygonCollider2D>();
        yelloSpriteRenderer = yello.GetComponent<SpriteRenderer>();
        yelloLs = yello.GetComponent<Light2D>();
        yelloTr = yello.GetComponent<TrailRenderer>();
        line = GameObject.Find("Line");
        lineRenderer = line.GetComponent<LineRenderer>();
        
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.R) && yello.GetComponent<PolygonCollider2D>().enabled)
        {
            StartCoroutine(AttachTimer());
            
        }

        if (Input.GetKeyDown(KeyCode.Return) && canShoot)
        {
            jellowProjectile2.GetComponent<JellowProjectileScript>().currentState = JellowProjectileScript.ProjectileState.fire;
            canShoot = false;
            Invoke("ActivateCollider", 0.25f);
        }
        

    }

    
    IEnumerator AttachTimer()
    {
        yelloSpriteRenderer.color = new Color(255, 255, 255, 0);
        yelloCollider.enabled = false;
        lineRenderer.enabled = false;
        jellowProjectile2 = Instantiate(jellowProjectile, yello.transform.position, Quaternion.identity);
        yelloHB = GameObject.FindGameObjectWithTag("YelloHB").GetComponent<Image>();
        yelloHB.enabled = false;
        yelloTr.enabled = false;
        yelloLs.enabled = false;
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
