using TMPro;
using UnityEngine;

public class Puzzle1_info : MonoBehaviour
{
    Collect Display;
    public GameObject Player;
    public TextMeshProUGUI Story;
    public TextMeshProUGUI Objective;

    private void Start()
    {
       Display = new Collect();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Objective.text =  "Find three links to the past, present and future and place them before the door to be granted passage into Hiems: \n" +
                "1) The Sword of the forest was created to stop the impending doom somehow spurred by the mystical treasure. \n" +
                "2) A crew of explorers who went after the treasure mysteriously vanished leaving behind only a flashlight. \n" +
                "3) A brilliant blue shard sits idly with the word 'Letum' scraped on the side.";
            // Debug.Log("Walked over plane");

            Story.text = "Our story begins with young Eve who is afflited with the Ardere sickness that is destroying her home; characterised by flaming yellow eyes. \n" +
                "Ancient stories were told through the generations about a mystical treasure in the destroyed city of Hiems beneath her home which has the power to grant any wish. \n" +
                "Eve ventures into the city with the hope of saving her home but something seems to be lurking in the darkness...";

            Display.sCollected = "Objective updated.";
        }

       // DisplayName.bDisplay = true;
    }
}
