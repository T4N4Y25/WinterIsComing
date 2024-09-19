using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : MonoBehaviour
{
    public GameObject Sword;
    public GameObject Flashlight;
    public GameObject Poison;
    public GameObject Player;
    private GameObject held;
    public GameObject Door;
    FirstPersonControls CurrentObject;
    string sCollected = "";
    bool bCollected;
    float fTime;
    GUIStyle style;
    bool PuzzleComplete;
    bool SwordComplete;
    bool FlashLightComplete;
    bool PoisonComplete;

    float raiseAmount;
    float raiseSpeed;
    Vector3 endPosition;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        CurrentObject = Player.GetComponent<FirstPersonControls>();
        style = new GUIStyle();
        style.normal.textColor = Color.white;
        style.fontSize = 18;
        style.fontStyle = FontStyle.BoldAndItalic;
        bCollected = false;
        fTime = 2f;
        PuzzleComplete = false;
        bool SwordComplete = false;
        bool FlashLightComplete = false;
        bool PoisonComplete = false;

         raiseAmount = 1.3f;
         raiseSpeed = 0.25f;
        endPosition = Door.transform.position + Vector3.up * raiseAmount ;
        startPosition = Door.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        held = CurrentObject.heldObject;

        if (held ==Sword )
        {
            Destroy( held );
            sCollected = "Sword of the forest collected.";
            bCollected=true;
            SwordComplete = true;
        }
        if (held == Flashlight)
        {
            Destroy(held);
            sCollected = "Forgotten crews flashlight collected.";
            bCollected = true;
            FlashLightComplete =true;

        }
        if (held == Poison)
        {
            Destroy(held);
            sCollected = "Poison vial collected.";
            bCollected = true;
            PoisonComplete = true;
        }

        if (fTime <= 0)
        {
            bCollected = false;
            fTime = 2f;
        }

        if( bCollected)
        {
            fTime -= Time.deltaTime;
        }

        if (SwordComplete== true && PoisonComplete == true && FlashLightComplete == true)
        {
            PuzzleComplete =true;
        }

        if(PuzzleComplete == true)
        {
            sCollected = "The door shudders open...";
        }

        if (PuzzleComplete == true)
        {
            Door.transform.position = Vector3.MoveTowards(Door.transform.position, endPosition, raiseSpeed * Time.deltaTime);

            
            if (Door.transform.position == endPosition)
            {
                PuzzleComplete = false; 
            }
        }
    }

    private void OnGUI()
    {
        if( bCollected ) 
        { 
            GUI.Label(new Rect(700, 10, 600, 200), sCollected,style);
        }
        else { 
            sCollected = ""; 
             }
        
    }

    
}
