using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public float bulletSpeed;
    private Rigidbody2D rb;
    private PlayerMovement player;
    public int bulletDamage = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = transform.right * bulletSpeed;
        }

        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>();
        }

        Collider2D bulletCollider = GetComponent<Collider2D>();
        Collider2D[] enemyColliders = FindObjectsOfType<EnemyController>()
            .Select(e => e.GetComponent<Collider2D>())
            .ToArray();

        foreach (var enemyCollider in enemyColliders)
        {
            Physics2D.IgnoreCollision(bulletCollider, enemyCollider);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (player != null)
            {
                player.HealthPoints = Mathf.Max(player.HealthPoints - bulletDamage, 0);
                Debug.Log("Player was hit!");
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector2 normal = collision.contacts[0].normal;

            Vector2 newDirection = Vector2.Reflect(rb.velocity.normalized, normal);

            rb.velocity = newDirection * bulletSpeed;

            float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Empe"))
        {
            Destroy(gameObject);
        }
    }
}

