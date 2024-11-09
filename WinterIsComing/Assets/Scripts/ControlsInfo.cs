using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlsInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tHUD;
    // Start is called before the first frame update
    void Start()
    {
       // tHUD.text = "Hello world";
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Player")))
        {
            tHUD.text = "WASD to move. \n" +
                "Press SHift to Dash. \n" +
                "Tab opens the menu and closes it again. \n" +
                "Once a weapon is equipped, left click to attack. \n" +
                "The enemy is too powerful in the labyrinth to defeat, if seen run as fast as you can. \n" +
                "GoodLuck!";
        }
        Debug.Log("Info displayed");
    }

    private void OnCollisionExit(Collision collision)
    {
        tHUD.text = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        tHUD.text = "WASD to move. \n" +
               "Press SHift to Dash. \n" +
               "Tab opens the menu and closes it again. \n" +
               "Once a weapon is equipped, left click to attack. \n" +
               "The enemy is too powerful in the labyrinth to defeat, if seen run as fast as you can. \n" +
               "GoodLuck!";
    }

    private void OnTriggerExit(Collider other)
    {
        tHUD.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
