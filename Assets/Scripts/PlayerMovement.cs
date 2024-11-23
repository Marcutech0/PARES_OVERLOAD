using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Animator anim;
    public Rigidbody2D rigidBody;
    public int healthPoints = 1000;
    public int maxHealth = 1000;
    public float moveSpeed;

    private Vector2 movementInput;
    private bool isDead = false;

    public GameObject shieldPrefab; 
    private GameObject activeShield;
    public Transform ShieldSpawn;
    public float shieldDuration = 15f;
    private bool hasShield = false;

    public GameObject deathScreen;

    public int HealthPoints
    {
        get { return healthPoints; }
        set
        {
            healthPoints = Mathf.Clamp(value, 0, 1000);
            if (healthPoints == 0 && !isDead)
            {
                StartCoroutine(HandleDeath());
            }
        }
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        deathScreen.SetActive(false);
    }

    void Update()
    {
        if (!isDead)
        {
            anim.SetFloat("Horizontal", movementInput.x);
            anim.SetFloat("Vertical", movementInput.y);
            anim.SetFloat("Speed", movementInput.sqrMagnitude);
        }
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            rigidBody.velocity = movementInput * moveSpeed;
        }
        else
        {
            rigidBody.velocity = Vector2.zero;
        }
    }

    private void OnMove(InputValue inputValue)
    {
        if (!isDead)
        {
            movementInput = inputValue.Get<Vector2>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Health"))
        {
            Destroy(collision.gameObject);
            HealthPoints += 1000;
        }

    }

    private IEnumerator HandleDeath()
    {
        isDead = true;
        anim.SetTrigger("death");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(2f);
        deathScreen.SetActive(true);
    }

    public void ActivateShield()
    {
        if (activeShield == null) 
        {
            activeShield = new GameObject("ShieldParent"); 
            activeShield.transform.position = transform.position; 

            int shieldCount = 5; 
            float angleStep = 360f / shieldCount; 
            float shieldDistance = 150f;

            for (int i = 0; i < shieldCount; i++)
            {
                float angle = i * angleStep;
                Vector2 shieldPosition = new Vector2(
                    transform.position.x + Mathf.Cos(angle * Mathf.Deg2Rad) * shieldDistance,
                    transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad) * shieldDistance
                );

                GameObject shield = Instantiate(shieldPrefab, shieldPosition, Quaternion.identity, activeShield.transform);

                Rigidbody2D shieldRb = shield.GetComponent<Rigidbody2D>();
                HingeJoint2D hinge = shield.GetComponent<HingeJoint2D>();

                if (shieldRb != null && hinge != null)
                {
                    hinge.connectedBody = GetComponent<Rigidbody2D>();
                    hinge.connectedAnchor = new Vector2(
                        Mathf.Cos(angle * Mathf.Deg2Rad) * shieldDistance,
                        Mathf.Sin(angle * Mathf.Deg2Rad) * shieldDistance
                    );
                }
            }
            StartCoroutine(ShieldTimer());
        }
    }

    private IEnumerator ShieldTimer()
    {
        yield return new WaitForSeconds(shieldDuration);
        DeactivateShield();
    }

    public void DeactivateShield()
    {
        if (activeShield != null)
        {
            Destroy(activeShield); 
            activeShield = null;  
        }
        hasShield = false; 
    }

    public void TakeDamage(int damage)
    {
        healthPoints -= damage;
        healthPoints = Mathf.Clamp(healthPoints, 0, maxHealth);
    }

    public void Heal(int healAmount)
    {
        healthPoints += healAmount;
        healthPoints = Mathf.Clamp(healthPoints, 0, maxHealth);
    }
}
