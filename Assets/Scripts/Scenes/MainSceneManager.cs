using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public void MainGameSceneLoad()
    {          
        SceneManager.LoadScene("MainGame");
    }

    public void MainMenuQuit()
    {
        Application.Quit();
    }
}
