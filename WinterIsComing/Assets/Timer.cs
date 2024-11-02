using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Needed for scene management
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedtime;
    float timeLimit = 60f; // 3 minutes in seconds

    // Update is called once per frame
    void Update()
    {
        // Only update if the elapsed time is less than the time limit
        if (elapsedtime < timeLimit)
        {
            elapsedtime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedtime / 60);
            int seconds = Mathf.FloorToInt(elapsedtime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

            // Check if we've reached the time limit
            if (elapsedtime >= timeLimit)
            {
                // Load the next scene
                
                SceneManager.LoadScene("BossLevel");
            }
        }
    }
}
