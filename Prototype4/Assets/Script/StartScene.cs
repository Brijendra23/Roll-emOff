using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
  
    
    public void StartMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
