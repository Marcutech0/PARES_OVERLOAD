using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public Transform player; 
    public GameObject bulletPrefab; 
    public Transform shootingPoint;
    private PointsManager pointsManager;

    public float moveSpeed; 
    public float EnemyHealth = 100f;

    public float shootingInterval; 
    public float bulletSpeed; 
    public float sightRange; 

    public float stopDistance = 1f; 
    private float shootingTimer = 0f;

    public delegate void EnemyDestroyed();
    public event EnemyDestroyed OnEnemyDestroyed;

    public GameObject[] itemPrefabs;
    public float[] dropChances;

    private void Start()
    {
        pointsManager = FindObjectOfType<PointsManager>();
    }

    private void OnDestroy()
    {
        if (pointsManager != null)
        {
            pointsManager.AddPoints(100); 
        }
    }

    void Update()
    {
        if (player != null && Vector2.Distance(transform.position, player.position) <= sightRange)
        {
            FollowPlayer();
            shootingTimer += Time.deltaTime;
            if (shootingTimer >= shootingInterval)
            {
                Shoot();
                shootingTimer = 0f;
            }
        }
        else
        {
            StopFollowing();
        }

        if (EnemyHealth <= 0)
        {
            Destroy(gameObject);
            OnEnemyDestroyed?.Invoke();
            ItemDrop();
        }
    }

    void FollowPlayer()
    {

        Vector2 direction = (player.position - transform.position).normalized;


        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, sightRange);


        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            return;
        }

        if (Vector2.Distance(transform.position, player.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    void StopFollowing()
    {

    }

    void Shoot()
    {
        if (player != null && bulletPrefab != null)
        {

            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, Quaternion.identity);

            Vector2 direction = (player.position - shootingPoint.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * bulletSpeed;
            }

            Destroy(bullet, 3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            PlayerBulletHandler bullet = collision.gameObject.GetComponent<PlayerBulletHandler>();
            if (bullet != null)
            {
                EnemyHealth -= bullet.damage;
                Debug.Log($"Enemy hit! Remaining Health: {EnemyHealth}");
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    void ItemDrop()
    {
        DropItem();
        Destroy(gameObject);
        OnEnemyDestroyed?.Invoke();
    }

    void DropItem()
    {
        float roll = Random.Range(0f, 6f);
        float cumulativeChance = 0f;

        for (int i = 0; i < itemPrefabs.Length; i++)
        {
            cumulativeChance += dropChances[i];
            if (roll <= cumulativeChance)
            {
                Instantiate(itemPrefabs[i], transform.position, Quaternion.identity);
                Debug.Log($"Dropped item: {itemPrefabs[i].name}");
                return;
            }
        }
    }
}
