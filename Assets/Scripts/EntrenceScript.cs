using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrenceScript : MonoBehaviour
{
    public float delay;
    public string SceneName;
    bool entering;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(SceneName);

    }
    IEnumerator enterTimer()
    {
        yield return new WaitForSeconds(delay);
    }
}
