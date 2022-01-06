using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneLoader : MonoBehaviour
{
    LevelChangerScript lcs;

    private void Start()
    {
        lcs = GameObject.Find("LevelChanger").GetComponent<LevelChangerScript>();
    }

    public void BackToMenu()
    {
        lcs.fadeToLevel("MainMenu");
    }
    public void StartGame()
    {
        lcs.fadeToLevel("CutSceneBeginning");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
