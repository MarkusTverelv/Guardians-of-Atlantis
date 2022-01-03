using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class GemScript : MonoBehaviour
{
    GemCounterScript gemCounter;
    public enum GemColors {pink, yellow};
    public GemColors gemColor;
    public AudioClip pickUpClip;
    AudioSource source;
    scoreScript scoreBoard;
    private void Start()
    {
        gemCounter = GameObject.Find("GemCounter").GetComponent<GemCounterScript>();
        source = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        if (gameObject.layer.Equals(6))
            gemColor = GemColors.pink;
        else if (gameObject.layer.Equals(7))
            gemColor = GemColors.yellow;

        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool b1 = collision.gameObject.name == "Yello" && gameObject.layer.Equals(7);
        bool b2 = collision.gameObject.name == "Pinko" && gameObject.layer.Equals(6);

        //Debug.Log(collision.gameObject.name);
        //Debug.Log(gameObject.layer);
        if (b1 || b2)
        {
            gemCounter.gemCount--;
            if (b1)
            {
                source.pitch = 1f;
                source.volume = 0.2f;
            }
            else
            {
                source.pitch = 0.9f;
                source.volume = 0.2f;
            }
            source.PlayOneShot(pickUpClip);
            Destroy(gameObject);
        }
    }

}
