using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void BackToMainMenu()
    {
        DataManager.Instance.ResetData(); 
        SceneManager.LoadScene("Main Menu");
    }
}
