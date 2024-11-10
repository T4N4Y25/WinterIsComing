using UnityEngine;

public class TorchLight : MonoBehaviour
{
    public float detectionRadius = 5f; // Radius within which the torch will light up
    public Light torchLight; // Reference to the Light component on the torch
    public GameObject player; // Reference to the player

    void Start()
    {
        // Get the Light component attached to the torch
        torchLight = GetComponent<Light>();

        // Find the player in the scene (assuming the player has the tag "Player")
        player = GameObject.FindGameObjectWithTag("Player");

        // Make sure the light is initially off
        if (torchLight != null)
        {
            torchLight.enabled = false;
        }
    }

    void Update()
    {
        // Check if the player is within the detection radius
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRadius)
        {
            // Enable the torch light
            if (torchLight != null)
            {
                torchLight.enabled = true;
            }
        }
        else
        {
            // Disable the torch light if the player is out of range
            if (torchLight != null)
            {
                torchLight.enabled = false;
            }
        }
    }
}
