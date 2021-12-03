using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CheckpointScript : MonoBehaviour
{
    Light2D light2D;
    PlayerSharedScript playerShared;
    // Start is called before the first frame update
    void Start()
    {
       

        light2D = GetComponent<Light2D>();
        playerShared = GameObject.Find("Players").GetComponent<PlayerSharedScript>();
        playerShared.onCheckpointSet.AddListener(SetLightColor);

        if (playerShared.checkpoint == gameObject)
            light2D.color = Color.green;
        else
            light2D.color = Color.blue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("crystal");
        if(collision.gameObject.CompareTag("Player"))
        {
            playerShared.SetCheckPoint(gameObject);
            playerShared.onCheckpointSet.Invoke();
        }
    }
    void SetLightColor()
    {
        if (playerShared.checkpoint == gameObject)
            light2D.color = Color.green;
        else
            light2D.color = Color.blue;
    }
}
