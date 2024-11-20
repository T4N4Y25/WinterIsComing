using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Collect : MonoBehaviour
{
    public GameObject Sword;
    public GameObject HeldSword;
    public GameObject Flashlight;
    public GameObject Poison;
    public GameObject Player;
    private GameObject held;
    public GameObject Door;
    FirstPersonControls CurrentObject;
    public string sCollected = "";
    bool bCollected;
    float fTime;
    GUIStyle style;
    bool PuzzleComplete;
    public bool SwordComplete;
    public bool FlashLightComplete;
    public bool PoisonComplete;
    [SerializeField] TextMeshProUGUI tObjective;
    [SerializeField] float raiseAmount = 2f;
    float raiseSpeed;
    Vector3 endPosition;
    Vector3 startPosition;
    [SerializeField] float OpenDistance = 5f;
    [SerializeField] TextMeshProUGUI StoryUpdate;
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

        // raiseAmount = 1.3f;
         raiseSpeed = 0.25f;
        endPosition = Door.transform.position + Vector3.up * raiseAmount ;
        startPosition = Door.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        held = CurrentObject.heldObject;
        tObjective.text = sCollected;
        if (held ==Sword )
        {
            Destroy( held );
            sCollected = "Sword: \n" +
                "Forged from winter steel, this sword  blazes with  fire. It�s the only weapon that can pierce the monster�s icy shield and launch searing fireballs that slow its advance.\r\n";
            bCollected=true;
            SwordComplete = true;
            HeldSword.SetActive( true );
            SceneManager.LoadScene("BossLevel");
        }
        if (held == Flashlight)
        {
            Destroy(held);
            sCollected = "Spell book:" +
                "\nBound with ancient magic, this book holds defensive spells to help evade  the boss�s attacks. giving you a moment to strategize  and counterattack.\r\n";
            bCollected = true;
            FlashLightComplete =true;

        }
        if (held == Poison)
        {
            Destroy(held);
            sCollected = "Vial: \n" +
                "A purple  liquid inside, the �Essence of Sight� reveals hidden paths and weak spots on the boss. During battle, it helps you see through the fog, tracking the monster�s movements.";
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

        if (SwordComplete== true  && FlashLightComplete == true)
        {
            PuzzleComplete =true;
        }

        if(PuzzleComplete == true)
        {
           // sCollected = "The door shudders open...";
        }

        if (Vector3.Distance(Player.transform.position, Door.transform.position) <= OpenDistance)
        {
            Door.transform.position = Vector3.MoveTowards(Door.transform.position, endPosition, raiseSpeed * Time.deltaTime);

            
            if (Door.transform.position == endPosition)
            {
                PuzzleComplete = false; 
            }
        }

        

        if(PoisonComplete == true)
        {
            
            StoryUpdate.text = "As you enter the fog laden room, pillars stretch up to darkness. The torches flicker around the throne, and the boss stirs. With the sword, vial, and spell book in hand, you�re ready to face its wrath before time runs out.";
        }
    }

    //private void OnGUI()
    //{
      //  if( bCollected ) 
      //  { 
      //      GUI.Label(new Rect(700, 10, 600, 200), sCollected,style);
      //  }
       // else { 
         //   sCollected = ""; 
         //    }
        
   // }

    
}
