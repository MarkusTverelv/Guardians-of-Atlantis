using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    [SerializeField] GameObject optionsMenu;
    public void Menu()
    {
        Debug.Log("Menu");
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
