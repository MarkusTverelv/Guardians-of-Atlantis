using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarWallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float wait, speed;
    void Start()
    {
        StartCoroutine(Chase());
    }

    IEnumerator Chase()
    {
        yield return new WaitForSeconds(wait);
        while(true)
        {
            transform.position += new Vector3(speed, 0);
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().hurt();
        }
    }
}
