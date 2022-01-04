using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySmallerExplosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Yello") || collision.gameObject.CompareTag("Pinko"))
        {
            collision.gameObject.transform.parent.GetComponent<PlayerSharedScript>().TakeDamage();
        }
    }

}
