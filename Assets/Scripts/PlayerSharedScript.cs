using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerSharedScript : MonoBehaviour
{
    public GameObject spawnPoint, checkPoint;
    SpawnPointScript spawnPointScript;
    public UnityEvent onCheckpointSet = new UnityEvent();
    public GameObject playersPrefab;
    public float moveSpeed, turnSpeed;
    GameObject spawn;
    GameObject[] players;
    Rigidbody2D[] bodies;
    CircleCollider2D yelloCollider;
    CircleCollider2D pinkoCollider;
    private void Awake()
    {
        if(GameObject.Find("SpawnPoint")==null)
        {
            GameObject spawn = Instantiate(spawnPoint);
            if (SceneManager.GetActiveScene().name == "Puzzle_1")
                spawnPoint.transform.position = new Vector3(-45, -15, 0);
        }

    }
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
            spawn = GameObject.Find("SpawnPoint");
            if (spawn.transform.position != null)
            {
                transform.position = spawn.transform.position;
                Debug.Log(transform.position);
            }
                
        }
        catch 
        {
            Debug.Log("NoSpawn");
        }
        

        Physics2D.IgnoreCollision(yelloCollider, pinkoCollider);
        spawnPointScript = spawnPoint.GetComponent<SpawnPointScript>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
   
    public void SetCheckPoint(GameObject gameObject)
    {
        spawn.transform.position = gameObject.transform.position;
        onCheckpointSet.Invoke();
        checkPoint = gameObject;
        Debug.Log("checkpoint set: " + gameObject);
    }
}
