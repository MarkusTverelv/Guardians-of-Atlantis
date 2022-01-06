using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsMenuScript : MonoBehaviour
{

    [SerializeField] GameObject fullscreenOption, volumeOption, resolutionOption, pauseMenu;
    TMP_Text volumeText, fullscreenText, resolutionText;
    Vector2Int[] resolutions = new Vector2Int[] 
    {
        new Vector2Int(1280, 720),
        new Vector2Int(1366, 768),
        new Vector2Int(1600, 900),
        new Vector2Int(1920, 1080)
    };
    Vector2Int _currentResolution;
    int _currentResolutionIndex;
    int currentResolutionIndex
    {
        get
        {
            return _currentResolutionIndex;
        }
        set
        {
            if(value>=0 && value<resolutions.Length)
            {
                _currentResolutionIndex = value;
                currentResolution = resolutions[value];
            }
            
        }
    }
    Vector2Int currentResolution
    {
        get
        {
            return _currentResolution;
        }
        set
        {
            _currentResolution = value;
            Screen.SetResolution(value.x, value.y, Screen.fullScreen);
            resolutionText.text = string.Format("Resolution: {0}x{1}", value.x, value.y);

        }
    }
    bool _fullscreen;
    bool fullScreen
    {
        get
        {
            return _fullscreen;
        }
        set
        {
            _fullscreen = value;
            Screen.fullScreen = value;
            if (value)
                fullscreenText.text = "Fullscreen: On";
            else
                fullscreenText.text = "Fullscreen: Off";

        }
    }
    


    List<float> baseAudioLevels = new List<float>();
    MenuScript menuScript;
    // Start is called before the first frame update
    int _volume = 5;
    int volume
    {
        get
        {
            return _volume;
        }
        set
        {
            Debug.Log(value);
            if (value >= 0 && value <= 10)
            {
                _volume = value;
                AudioListener.volume = value / 10f;
                volumeText.text = string.Format("Volume: {0}", volume);
            }
            
             

        }
    }
    void Start()
    {
        
        //currentResolution = resolutions[3];
        volumeText = volumeOption.GetComponent<TMP_Text>();
        fullscreenText = fullscreenOption.GetComponent<TMP_Text>();
        resolutionText = resolutionOption.GetComponent<TMP_Text>();
        menuScript = GetComponent<MenuScript>();
        Debug.Log(menuScript.selectedScripts);
        Debug.Log("test");
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject selection = menuScript.selectedScripts[menuScript.selectedOption].gameObject;
        if (selection == volumeOption)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                volume--;
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                volume++;
        }
        else if(selection == fullscreenOption)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                fullScreen = !fullScreen;
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                fullScreen = !fullScreen;
        }
        else if(selection == resolutionOption)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                currentResolutionIndex++;
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                currentResolutionIndex--;
        }
    }
    public void Fullscreen()
    {
        fullScreen = !fullScreen;        
    }
    public void Back()
    {
        pauseMenu.SetActive(true);
        gameObject.SetActive(false);
    }

}
