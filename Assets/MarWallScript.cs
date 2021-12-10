using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarWallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float wait, speed;
    public AudioClip chaseMusic, mainMusic;
    public AudioSource musicplayer;
    NewPinkoMovement newPinkoMovement;
    NewYelloMovement newYelloMovement;
    void Start()
    {
        StartCoroutine(Chase());
        newPinkoMovement = GameObject.Find("Pinko").GetComponent<NewPinkoMovement>();
        newYelloMovement = GameObject.Find("Yello").GetComponent<NewYelloMovement>();
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
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Yello"))
        {
            newYelloMovement.TakeDamage();
        }
        else if (collision.gameObject.CompareTag("Pinko"))
        {
            newPinkoMovement.TakeDamage();
        }
    }
}
