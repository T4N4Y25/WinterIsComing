using TMPro;
using UnityEngine;

public class Puzzl2Info : MonoBehaviour
{
    // Reference to the player's controls script
    FirstPersonControls DisplayName;
    Collect CollectMessage;
    public GameObject Player;
    public GameObject Door1;
    public TextMeshProUGUI Story;
    public TextMeshProUGUI Objective;

    private void Start()
    {
        DisplayName = Player.GetComponent<FirstPersonControls>();
        CollectMessage = Door1.GetComponent<Collect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Objective.text = "The swift and crafty were able to rise to the ranks of nobility as they were able to catch the creatures responsible for the mystical treasures power. \n" +
                "Prove yourself by reaching the other side of the maze in the given time while avoiding obstacles and threats. \n" +
                "To begin press the switch. \n" +
                "Puzzle is not finished...";
            CollectMessage.sCollected = "Objective updated.";
            // Debug.Log("Walked over plane");

            Story.text = "After learning more about the city Eve has more questions than answers. What was the Sword made to stop? Who or what killed the missing crew? What does 'Letum', the message on the shard mean? \n" +
                "Eve enters the city and is met with the smell of blood, death traps everywhere. \n" +
                "What were the purpose of the traps? Were they to keep out intruders from stealing the mystical treasure or perhaps they were used to catch something...";
        }

        // DisplayName.bDisplay = true;
    }
}
