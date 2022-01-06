using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject creditsMenu;

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void Options()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void Credits()
    {
        creditsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void ReturnFromCredits()
    {
        creditsMenu.SetActive(false);
        gameObject.SetActive(true);
    }
}
