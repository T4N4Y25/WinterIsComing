using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TextBox;
    [SerializeField] string[] Speech;
   // [SerializeField] CharacterController playercontrols;
    [SerializeField] GameObject Player;
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform[] FairyPos;
    private float pickUpRange = 1f;
    int i = 0;
    bool bDialogue = false;
    // Start s called before the first frame update
    void Start()
    {
        TextBox.enabled = false;
    }

    public void InteractWithNPC()
    {
        
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        
        Debug.DrawRay(playerCamera.position, playerCamera.forward * pickUpRange, Color.green, 2f);

        
        if (Physics.Raycast(ray, out hit, pickUpRange))
        {
            
            if (hit.collider.CompareTag("NPC"))
            {
                bDialogue = true;
                

            }
        }
    }

    void Detect()
    {
        if(Vector3.Distance(Player.transform.position,this.transform.position) <= pickUpRange +2)
        {
            TextBox.enabled = true;
        }
        else
        {
            TextBox.enabled = false;
        }
    }


    public void Next()
    {
        i++;
        Debug.Log("Next");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            InteractWithNPC();
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E) && TextBox.enabled)
        {
            i++;
        }

        if (i > Speech.Length - 2)
        {
            TextBox.enabled = false;
            this.transform.position = Vector3.MoveTowards(this.transform.position, FairyPos[1].position, 0.5f*Time.deltaTime);
        }

        if (bDialogue)
        {
            TextBox.text = Speech[i];
            this.transform.position = Vector3.MoveTowards(this.transform.position, FairyPos[0].position, 0.5f * Time.deltaTime);
        }
        else if(!bDialogue)
        {
            TextBox.text = "Hey over here!";
        }
        Detect();
    }
}
