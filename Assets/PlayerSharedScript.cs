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
    SpawnPointScript spawn;
    private void Awake()
    {
        spawn = GameObject.Find("SpawnPoint").GetComponent<SpawnPointScript>();

        if (spawn.position != null)
            transform.position = spawn.position+=new Vector3(-30,0);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    public void SetCheckPoint(GameObject gameObject)
    {
        spawn.position = gameObject.transform.position;  
        onCheckpointSet.Invoke();
        checkpoint = gameObject;
        Debug.Log("checkpoint set: " + gameObject);
    }
}
