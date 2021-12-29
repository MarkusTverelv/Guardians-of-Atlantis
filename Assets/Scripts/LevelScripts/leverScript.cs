using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leverScript : MonoBehaviour
{
    public bool imActive;
    float leverTimer;
    private GameObject gm;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameMaster");
    }

    // Update is called once per frame
    

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinko") || collision.gameObject.CompareTag("Yello"))
        {
            if (Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.F))
            {
                gm.GetComponent<AudioScript>().playLeverSound();
                imActive = true;
                animator.SetTrigger("LeverMove");
            }
        }
    }
}
