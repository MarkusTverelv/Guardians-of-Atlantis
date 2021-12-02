using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerSharedScript : MonoBehaviour
{
    public GameObject checkpoint;
    public UnityEvent onCheckpointSet = new UnityEvent();
    public GameObject playersPrefab;
    public float moveSpeed, turnSpeed;
    SpawnPointScript spawn;
    GameObject[] players;
    Rigidbody2D[] bodies;
    CircleCollider2D yelloCollider;
    CircleCollider2D pinkoCollider;
    private void Start()
    {
        GameObject yello = GameObject.Find("Yello");
        GameObject pinko = GameObject.Find("Pinko");
        yelloCollider = yello.GetComponent<CircleCollider2D>();
        pinkoCollider = pinko.GetComponent<CircleCollider2D>();
        Rigidbody2D yelloBody = yello.GetComponent<Rigidbody2D>();

        Rigidbody2D pinkoBody = pinko.GetComponent<Rigidbody2D>();
        players = new GameObject[] { pinko, yello };
        bodies = new Rigidbody2D[] { yelloBody, pinkoBody };
        try
        {
            spawn = GameObject.Find("SpawnPoint").GetComponent<SpawnPointScript>();
            if (spawn.position != null)
                transform.position = spawn.position;
        }
        catch { }
        

        Physics2D.IgnoreCollision(yelloCollider, pinkoCollider);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    private void FixedUpdate()
    {
        float distance = Vector2.Distance(players[0].transform.position, players[1].transform.position);
        Vector2 center = (players[0].transform.position + players[1].transform.position) / 2;
        float springFactor;
        
        //if (distance > bondLenght)
        //    springFactor = distance;
        //else
        //    springFactor = bondLenght - distance;
        ////foreach (Rigidbody2D body in bodies)
        ////    body.AddForce((center - (Vector2)body.transform.position) * springFactor * bondForce);
        //Debug.Log("springfactor: " + springFactor + ",      distance: " + distance);
    }
    public void SetCheckPoint(GameObject gameObject)
    {
        spawn.position = gameObject.transform.position;
        onCheckpointSet.Invoke();
        checkpoint = gameObject;
        Debug.Log("checkpoint set: " + gameObject);
    }
}
