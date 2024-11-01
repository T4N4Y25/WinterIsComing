using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float elapsedtime;
    // Update is called once per frame
    void Update()
    {
        elapsedtime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedtime / 60);
        int seconds = Mathf.FloorToInt(elapsedtime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
