using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverScript : MonoBehaviour
{
    public bool imActive;
    float leverTimer;
    private GameObject gm;
    public Animator animator;
    public GameObject interactPinkoText;
    public GameObject interactYelloText;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster");
    }

    // Update is called once per frame
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinko"))
        {
            interactPinkoText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.F))
            {
                gm.GetComponent<AudioScript>().playLeverSound();
                imActive = true;
                animator.SetTrigger("LeverMove");
            }
        }

        if(collision.gameObject.CompareTag("Yello"))
        {
            interactYelloText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                gm.GetComponent<AudioScript>().playLeverSound();
                imActive = true;
                animator.SetTrigger("LeverMove");
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
