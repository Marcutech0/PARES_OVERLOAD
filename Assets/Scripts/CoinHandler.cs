using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PointsManager pointsManager = FindObjectOfType<PointsManager>();

            if (pointsManager != null)
            {
                pointsManager.CollectCoin(); 
            }

            Destroy(gameObject); 
        }
    }
}
