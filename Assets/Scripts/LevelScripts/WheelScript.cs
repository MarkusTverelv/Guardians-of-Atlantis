using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelScript : MonoBehaviour
{
    public float rotz2;
    public float rotz1;
    bool goBack, goBack2;
    public GameObject interactPinkoText;
    public GameObject interactYelloText;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        if(goBack2)
        {
            if (rotz1 > 0)
            {
                transform.Rotate(0, 0, -1);
                rotz1 -= 50 * Time.deltaTime;
            }
        }

        if (goBack)
        {
            if (rotz2 > 0)
            {
                transform.Rotate(0, 0, -1);
                rotz2 -= 50 * Time.deltaTime;
            }
        }
    }

    // Update is called once per frame

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("Pinko"))
        {
            interactPinkoText.SetActive(true);
            if (Input.GetKey(KeyCode.F))
            {
                goBack = false;

                if (rotz2 < 360)
                    rotz2 += 100f * Time.deltaTime;
                transform.Rotate(0, 0, 1);

            }

            else
            {
                goBack = true;
            }

        }

        if (collision.gameObject.CompareTag("Yello"))
        {
            interactYelloText.SetActive(true);
            if (Input.GetKey(KeyCode.Keypad4))
            {
                goBack2 = true;
                if (rotz1 < 360)
                    rotz1 += 100f * Time.deltaTime;
                transform.Rotate(0, 0, 1);

            }

            else
            {
                goBack2 = true;
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinko"))
        {
            interactPinkoText.SetActive(false);
        }

        if (collision.gameObject.CompareTag("Yello"))
        {
            interactYelloText.SetActive(false);
        }
    }
}
