using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class CutSceneController : MonoBehaviour
{
    VideoPlayer vPlayer;

    private void Awake()
    {
        vPlayer = GetComponent<VideoPlayer>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeScene", 31f);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeScene();
        }
    }

    private void ChangeScene()
    {
        SceneManager.LoadScene("Level");
    }
}
