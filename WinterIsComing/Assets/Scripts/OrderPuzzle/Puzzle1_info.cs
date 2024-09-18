using UnityEngine;

public class Puzzle1_info : MonoBehaviour
{
    // Reference to the player's controls script
    FirstPersonControls DisplayName;
    public GameObject Player;

    private void Start()
    {
        DisplayName = Player.GetComponent<FirstPersonControls>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayName.DisplayMessage = "Find three links to the past, present and future and place them before the door to proceed: \n" +
                "1) The Sword of the forest once kept this ancient city safe from the evils lurking within... \n" +
                "2) A crew of explorers mysteriously vanished leaving behind only a flashlight amidst a red room... \n" +
                "3) Only a rare, blue vial of poison can stop the creatures from destroying the city's future...";
           // Debug.Log("Walked over plane");
        }

       // DisplayName.bDisplay = true;
    }
}
