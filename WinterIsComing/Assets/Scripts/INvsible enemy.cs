using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INvsibleenemy : MonoBehaviour
{
    public GameObject enemy; // Assign the enemy GameObject in the Inspector

    private void Start()
    {
        // Ensure the enemy is invisible at the start
        if (enemy != null)
        {
            MeshRenderer enemyRenderer = enemy.GetComponent<MeshRenderer>();
            if (enemyRenderer != null)
            {
                enemyRenderer.enabled = false; // Hide the enemy initially
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object has the specific tag
        if (other.CompareTag("PickUp"))
        {
            // Reveal the enemy by enabling its Mesh Renderer
            if (enemy != null)
            {
                MeshRenderer enemyRenderer = enemy.GetComponent<MeshRenderer>();
                if (enemyRenderer != null)
                {
                    enemyRenderer.enabled = true; // Make the enemy visible
                }
            }

            // Optionally destroy the pickup object
            Destroy(other.gameObject);
        }
    }

}
