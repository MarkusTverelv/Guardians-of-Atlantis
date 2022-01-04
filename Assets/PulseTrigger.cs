using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTrigger : MonoBehaviour
{
    PulseScript ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = transform.parent.GetComponentInParent<PulseScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            ps.AddForce(other);
        }

        if (other.gameObject.CompareTag("Bomb"))
        {
            ps.AddForce(other);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bomb"))
        {
            Destroy(other.gameObject, 6);
        }
    }
}
