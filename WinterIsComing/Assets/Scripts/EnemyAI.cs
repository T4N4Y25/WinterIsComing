using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private Transform player;  // Reference to the player transform
    [SerializeField] private float startSpeed = 1f;  // Initial speed of the AI
    [SerializeField] private float maxSpeed = 5f;    // Maximum speed AI will reach as the timer expires
    [SerializeField] private float timerDuration = 30f;  // Time duration in seconds for AI to reach max speed
    [SerializeField] private float obstacleAvoidanceDistance = 2f;  // Distance to detect walls
    [SerializeField] private float avoidStrength = 5f;  // Strength of wall avoidance steering

    private float currentSpeed;
    private float timer;

    private void Start()
    {
        currentSpeed = startSpeed;
        timer = 0f;
    }

    private void Update()
    {
        // Update timer, but don't exceed timerDuration
        if (timer < timerDuration)
        {
            timer += Time.deltaTime;
        }

        // Calculate the current speed based on the timer
        currentSpeed = Mathf.Lerp(startSpeed, maxSpeed, timer / timerDuration);

        // Calculate direction toward the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Check for obstacles in front of the AI
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, obstacleAvoidanceDistance))
        {
            if (hit.collider.CompareTag("Construction"))  // Check if the obstacle is a wall
            {
                // Calculate a direction to avoid the wall by steering to the side
                Vector3 avoidDirection = Vector3.Cross(hit.normal, Vector3.up) * avoidStrength;
                directionToPlayer += avoidDirection.normalized;
            }
        }

        // Move the AI toward the player while avoiding obstacles
        transform.position += directionToPlayer.normalized * currentSpeed * Time.deltaTime;

        // Rotate the AI to face the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * currentSpeed);
    }
}
