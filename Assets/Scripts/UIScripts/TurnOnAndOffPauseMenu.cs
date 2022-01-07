using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnAndOffPauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;

    private bool onOff = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !optionsMenu.activeSelf)
        {
            onOff = !onOff;
            pauseMenu.SetActive(onOff);
            if (onOff)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
