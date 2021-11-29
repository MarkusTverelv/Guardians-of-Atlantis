using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShootScript : MonoBehaviour
{
    GameObject yello, line, arrowDirection, jellowProjectile2;
    [SerializeField] GameObject jellowProjectile;
    SpriteRenderer yelloSpriteRenderer, arrowSpriteRenderer;
    CapsuleCollider2D capsuleCollider2D;
    LineRenderer lr;
    SpriteRenderer yelloSprite;
    CapsuleCollider2D yelloCapsule;
    LineRenderer lineRenderer;
    JellowProjectileScript jellowProjectileScript;
    bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        arrowDirection = GameObject.Find("ArrowAim");
        
        yello = GameObject.Find("Yello");
        line = GameObject.Find("Line");
        lr = line.GetComponent<LineRenderer>();
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
        yello.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        yello.GetComponent<PolygonCollider2D>().enabled = false;
        line.GetComponent<LineRenderer>().enabled = false;
        jellowProjectile2 = Instantiate(jellowProjectile, yello.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        jellowProjectile2.GetComponent<JellowProjectileScript>().currentState = JellowProjectileScript.ProjectileState.idle;
        canShoot = true;
        arrowDirection.GetComponent<SpriteRenderer>().enabled = true;

    }

    private void ActivateCollider()
    {
        jellowProjectile2.GetComponent<CircleCollider2D>().enabled = true;
    }
    
}
