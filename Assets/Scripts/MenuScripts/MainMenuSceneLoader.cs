using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneLoader : MonoBehaviour
{
    LevelChangerScript lcs;
    public AudioSource music;

    private void Start()
    {
        lcs = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
    }

    public void BackToMenu()
    {
        music.Stop();
        Time.timeScale = 1;
        lcs.fadeToLevel("MainMenu");
    }

    public void StartGame()
    {
        lcs.fadeToLevel("CutSceneBeginning");
    }

    public void StartBossFight()
    {
        lcs.fadeToLevel("Fluid");
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        Application.Quit();
    }
}