using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public GameObject pinko;
    public GameObject jellow;
    public GameObject line;
    private Transform attachPoint;
    public GameObject jellowProjectile;
    public GameObject arrowDirection;
    public LineRenderer lr;
    GameObject jellowProjectile2;
    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        attachPoint = transform.Find("JellowAttach");
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.R) && jellow.GetComponent<CapsuleCollider2D>().enabled)
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
        jellow.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        jellow.GetComponent<CapsuleCollider2D>().enabled = false;
        line.GetComponent<LineRenderer>().enabled = false;
        jellowProjectile2 = Instantiate(jellowProjectile, jellow.transform.position, Quaternion.identity);
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
