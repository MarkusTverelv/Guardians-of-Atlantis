using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseScript : MonoBehaviour
{
    LevelChangerScript lcs;

    //static public UnityEvent OnPause = new UnityEvent();
    //static public UnityEvent OnUnPause = new UnityEvent();
    static bool _isPaused;
    [SerializeField] AudioSource music;
    [SerializeField] AudioLowPassFilter muffleFilter;
    [SerializeField] Image pauseOverlay, pauseScreen;
    [SerializeField] GameObject pauseMenu, optionsMenu;
    
    private void Start()
    {
        pauseMenu.SetActive(false);
        lcs = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
    }
    public bool isPaused
    {
        get 
        {
            return _isPaused;  
        }
        set
        {
            _isPaused = value;
            pauseOverlay.enabled = value;
            //pauseScreen.enabled = value;
            muffleFilter.enabled = value;
            pauseMenu.SetActive(value);
            if (value)
            {
                music.volume = 0.5f;
                Time.timeScale = 0;
            }
                
            else
            {
                Time.timeScale = 1;
                music.volume = 1f;
            }
                

        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(!optionsMenu.activeSelf)
                isPaused = !isPaused;
            else
            {
                optionsMenu.SetActive(false);
                pauseMenu.SetActive(true);
            }
        }
    }
    public void MenuButton()
    {
        lcs.fadeToLevel("MainMenu");
    }

}
