using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class Note: MonoBehaviour
{




    public Camera playerCamera; // Player's camera
    public float interactionRange = 5f; // How far the player can interact
    public GameObject notePanel; // The UI panel displaying the note
    public TextMeshProUGUI noteText; // The text field to update
    private bool isPanelActive = false; // Tracks if the panel is active
    
    void Start()
    {
      
        notePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isPanelActive)
        {
            TryReadNote();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPanelActive)
        {
            HideNotePanel();
        }
    }

    private void TryReadNote()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            if (hit.collider.CompareTag("Note"))
            {
                DisplayNoteText(hit.collider.gameObject);
            }
        }
    }

    private void DisplayNoteText(GameObject noteObject)
    {
        scrip noteScript = noteObject.GetComponent<scrip>();
        if (noteScript != null)
         {
            notePanel.SetActive(true); // Show the note panel
            noteText.text = noteScript.noteContent; // Update the text field with the note's content
            isPanelActive = true; // Track that the panel is active
        }
    }

    private void HideNotePanel()
    {
        notePanel.SetActive(false);
        noteText.text = "";
        isPanelActive = false;
    }
}

