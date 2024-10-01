using UnityEngine;

public class Puzzl2Info : MonoBehaviour
{
    // Reference to the player's controls script
    FirstPersonControls DisplayName;
    Collect CollectMessage;
    public GameObject Player;
    public GameObject Door1;

    private void Start()
    {
        DisplayName = Player.GetComponent<FirstPersonControls>();
        CollectMessage = Door1.GetComponent<Collect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DisplayName.DisplayMessage = "Only the swift and crafty were able to escape to impending threat of the creatures, those not fast enough were left behind...\n" +
                "Prove yourself by reaching the other side of the maze in the given time while avoiding obstacles and threats. \n" +
                "To begin press the switch. \n" +
                "Puzzle is not finished...";
            CollectMessage.sCollected = "";
            // Debug.Log("Walked over plane");
        }

        // DisplayName.bDisplay = true;
    }
}
