using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    GameObject player;

    private void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void FixedUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.transform.position.z);
    }
}
