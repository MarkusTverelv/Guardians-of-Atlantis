using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour
{
    bool pinkoHasLeft;
    bool yelloHasLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Pinko"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            pinkoHasLeft = false;
        }

        if (collision.gameObject.CompareTag("Yello"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            yelloHasLeft = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinko"))
        {
            pinkoHasLeft = true;
            if (yelloHasLeft)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -100);
            }
        }

        if(collision.gameObject.CompareTag("Yello"))
        {
            yelloHasLeft = true;
            if (pinkoHasLeft)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -100);
            }
        }
    }
}
