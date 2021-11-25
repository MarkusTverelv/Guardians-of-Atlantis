using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Gem : MonoBehaviour
{
    public UnityEvent pickUp;
    public AudioClip pickUpClip;
    AudioSource source;
    scoreScript scoreBoard;
    private void Start()
    {
        source = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        scoreBoard = GameObject.Find("Score").GetComponent<scoreScript>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickUp?.Invoke();
            source.PlayOneShot(pickUpClip);
            scoreBoard.score++;
            Destroy(gameObject);
        }
    }
    

}
