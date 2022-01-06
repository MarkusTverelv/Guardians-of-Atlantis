using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCredits : MonoBehaviour
{
    public GameObject credits;
    public GameObject pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && credits.activeSelf)
        {
            credits.SetActive(false);
            pauseMenu.SetActive(true);
        }
    }
}
