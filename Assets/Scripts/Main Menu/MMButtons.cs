using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MMButtons : MonoBehaviour
{
  
    public void playGame()
    {
        SceneManager.LoadScene("2D Game Project");

    }
    public void QuitApp()
    {
        Application.Quit();
    }
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
