using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    public void Menu()
    {
        Debug.Log("Menu");
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void Options()
    {
        Debug.Log("Options");
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
