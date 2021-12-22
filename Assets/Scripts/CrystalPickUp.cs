using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalPickUp : MonoBehaviour
{
    public GameObject particle;
    public AudioClip pickUpSFX;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pinko") || other.gameObject.CompareTag("Yello"))
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(particle, 1);
            Destroy(gameObject);
        }
    }
}
