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
    public LineRenderer lr;
    
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
            lr.enabled = false;
            StartCoroutine(AttachTimer());
            
        }
        
    }

    
    IEnumerator AttachTimer()
    {
        jellow.SetActive(false);
        line.SetActive(false);
        Instantiate(jellowProjectile, jellow.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);

        // calculate distance to move
        jellowProjectile.transform.position = Vector3.MoveTowards(jellowProjectile.transform.position, attachPoint.position, 5 * Time.deltaTime);

    }

    
}
