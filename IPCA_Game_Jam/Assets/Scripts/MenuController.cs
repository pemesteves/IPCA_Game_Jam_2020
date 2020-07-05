using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject instructions;
    public GameObject credits;

    public void Play()
    {
        SceneManager.LoadScene("Game");
    }

    public void Instructions()
    {
        menu.SetActive(false);
        instructions.SetActive(true);
        credits.SetActive(false);
    }

    public void Credits()
    {
        menu.SetActive(false);
        instructions.SetActive(false);
        credits.SetActive(true);
    }

    public void Menu()
    {
        menu.SetActive(true);
        instructions.SetActive(false);
        credits.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
