using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioSource audioSourceDash;
    public AudioSource audioSourceLife;
    public AudioSource stoneSource;
    public AudioSource destinySource;
    public AudioSource leverSource;
    public AudioSource checkPointSource;
    public AudioSource explodeSource;
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

    public void playDestinySound()
    {
        destinySource.Play();
    }

    public void playLeverSound()
    {
        leverSource.Play();
    }

    public void playCheckPointSound()
    {
        checkPointSource.Play();
    }

    public void playExplodeSound()
    {
        explodeSource.Play();
    }

}
