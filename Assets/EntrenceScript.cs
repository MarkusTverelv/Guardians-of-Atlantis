using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrenceScript : MonoBehaviour
{
    public string SceneName;  
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(SceneName);

    }
}
