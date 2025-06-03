using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Play");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void FromOneToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
