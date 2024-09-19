using UnityEngine;

public class Puzzl2Info : MonoBehaviour
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
            DisplayName.DisplayMessage = "Only the swift and crafty were able to escape to impending threat of the creatures, those not fast enough were left behind...\n" +
                "Prove yourself by reaching the other side of the maze in the given time while avoiding obstacles and threats. \n" +
                "To begin press the switch.";
            // Debug.Log("Walked over plane");
        }

        // DisplayName.bDisplay = true;
    }
}
