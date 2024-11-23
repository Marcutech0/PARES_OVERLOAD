using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Transform player;   
    public float orbitSpeed = 500f;

    void Start()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            player = playerMovement.transform;
        }
        else
        {
            Debug.LogError("Shield could not find the player. Make sure there is a PlayerMovement script in the scene.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            transform.RotateAround(player.position, Vector3.forward, orbitSpeed * Time.deltaTime);
        }
    }
}
