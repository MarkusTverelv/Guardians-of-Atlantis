using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openLevel()
    {
        SceneManager.LoadScene("CutsceneBeginning");
    }

    public void openOptions()
    {

    }

    public void openCredits()
    {

    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void goBoss()
    {
        SceneManager.LoadScene("Fluid");
    }
}
