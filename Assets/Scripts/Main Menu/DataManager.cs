using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    public int totalPoints;
    public int coinsCollected;

    private void Awake()
    {
        // Ensure only one instance of DataManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to reset the data after each game
    public void ResetData()
    {
        totalPoints = 0;
        coinsCollected = 0;
    }
}
