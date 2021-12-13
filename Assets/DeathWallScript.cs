using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathWallScript : MonoBehaviour
{
    public float speed = 0.1f;
    public float wait = 1;
    private void Start()
    {
        StartCoroutine(deathWallMove());
    }
    IEnumerator deathWallMove() 
    {
        yield return new WaitForSeconds(wait);
        while(true)
        {
            transform.localScale += new Vector3(speed, 0);
            yield return new WaitForFixedUpdate();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            SceneManager.LoadScene("Level");
    }

}
