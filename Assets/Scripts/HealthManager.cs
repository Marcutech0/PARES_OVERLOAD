using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public RectTransform healthBar; 
    public float maxHealth = 1000f;
    private PlayerMovement player; 

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        if (player == null)
        {
            Debug.LogError("PlayerMovement not found");
            return;
        }
        UpdateHealthBar();
    }

    void Update()
    {
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (player != null && healthBar != null)
        {
            float healthPercentage = player.healthPoints / maxHealth;
            float newWidth = healthPercentage * 100f; 
            healthBar.sizeDelta = new Vector2(newWidth, healthBar.sizeDelta.y);
        }
    }
}