using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MiniMapScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Level")
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
