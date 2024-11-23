using System.Collections;
using UnityEngine;

public class Consumables : MonoBehaviour
{
    public bool Health, Dragon;
    public int HealthRegen;
    private PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player == null)
            {
                player = collision.GetComponent<PlayerMovement>();
            }

            // Apply health regeneration
            if (Health && player != null)
            {
                player.HealthPoints += HealthRegen;

                if (player.HealthPoints > 1000)
                {
                    player.HealthPoints = 1000;
                }
            }
            Destroy(gameObject);
        }
    }
}
