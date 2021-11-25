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
    JellowProjectileScript jellowScript = new JellowProjectileScript();

    // Start is called before the first frame update
    void Start()
    {
        attachPoint = transform.Find("JellowAttach");

        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(AttachTimer());
            
        }

        if (Input.GetKeyDown(KeyCode.Return) && canShoot)
        {
            jellowProjectile2.GetComponent<JellowProjectileScript>().currentState = JellowProjectileScript.ProjectileState.fire;
            canShoot = false;
            jellowProjectile2.GetComponent<CircleCollider2D>().enabled = true;

        }
        

    }

    
    IEnumerator AttachTimer()
    {
        jellow.SetActive(false);
        line.GetComponent<LineRenderer>().enabled = false;
        jellowProjectile2 = Instantiate(jellowProjectile, jellow.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(3);
        jellowProjectile2.GetComponent<JellowProjectileScript>().currentState = JellowProjectileScript.ProjectileState.idle;
        canShoot = true;
        Instantiate(arrowDirection, transform.position, Quaternion.identity, this.transform);

    }

    
}
