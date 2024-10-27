using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    // List of tags that, when collided with, will reset the game
    [SerializeField] private string[] resetTags;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object's tag is in the resetTags array
        foreach (string tag in resetTags)
        {
            if (other.CompareTag(tag))
            {
                ResetGame();
                return; // Exit the loop once reset condition is met
            }
        }
    }

    private void ResetGame()
    {
        // Reload the current active scene to reset the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
