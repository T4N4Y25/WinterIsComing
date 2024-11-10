using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour

{
    [SerializeField] GameObject door;
    [SerializeField] GameObject player;
    [SerializeField] float openDistance = 5f;
    [SerializeField] float raiseAmount = 2f;
    [SerializeField] float raiseSpeed = 0.25f;

    private Vector3 endPosition;
    private Vector3 startPosition;
    private bool isDoorOpen = false;

    void Start()
    {
        // Define the start and end positions for the door
        startPosition = door.transform.position;
        endPosition = door.transform.position + Vector3.up * raiseAmount;
    }

    void Update()
    {
        OpenDoorIfPlayerIsClose();
    }

    void OpenDoorIfPlayerIsClose()
    {
        // Check the distance between the player and the door
        if (Vector3.Distance(player.transform.position, door.transform.position) <= openDistance)
        {
            // Move the door upward towards the end position
            door.transform.position = Vector3.MoveTowards(door.transform.position, endPosition, raiseSpeed * Time.deltaTime);

            // Set the door open status to stop updating if it has reached the end position
            if (door.transform.position == endPosition)
            {
                isDoorOpen = true;
            }
        }
    }
}
