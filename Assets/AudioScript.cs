using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource audioSourceDash;
    public AudioSource audioSourceLife;
    public AudioSource stoneSource;
    private float talkTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playDashSound()
    {
        audioSourceDash.Play();
    }

    public void playLifeSound()
    {
        audioSourceLife.Play();
    }

    public void stoneSound()
    {
        stoneSource.Play();
    }

    public void stopStoneSound()
    {
        stoneSource.Stop();
    }

}
