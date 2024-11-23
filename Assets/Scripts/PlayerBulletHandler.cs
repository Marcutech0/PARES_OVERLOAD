using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerBulletHandler : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    public  float damage;
    private EnemyController enemyController;

    private void Start()
    {
        mainCam =  GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        Destroy(gameObject, 3f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("PlayerBullet"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
            return;
        }

        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
        }
    }
}
