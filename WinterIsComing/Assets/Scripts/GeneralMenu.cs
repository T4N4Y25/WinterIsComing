using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralMenu : MonoBehaviour
{
    [Header("Inventory Settings")]
    [Space(5)]
    public bool[] bInv = new bool[12];
    public GameObject gInvParent;
    public GameObject[] gInv = new GameObject[12];
    private Collect collectscript; 
    public Sprite SwordOfTheForest;
    public Sprite Flashlihgt;
    public Sprite Posion;
    private bool swordAddedToInventory = false; 
    private bool FlashlightAddedToInventory = false;
    private bool PoisonAddedToInventory = false;

    void Start()
    {
        // Initialize inventory status as empty and store all the inventory slots
        for (int i = 0; i < bInv.Length; i++)
        {
            bInv[i] = false;
            gInv[i] = gInvParent.transform.GetChild(i).gameObject;
        }

        // Assuming the Collect component is attached to the same GameObject or another object in the scene
        collectscript = FindObjectOfType<Collect>(); // Find the existing Collect component in the scene
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (collectscript.SwordComplete && !swordAddedToInventory)
        {
            AddSwordToInventory();
            collectscript.SwordComplete = false; 
        }

        if(collectscript.FlashLightComplete && !FlashlightAddedToInventory)
        {
            AddFlashlightToInventory();
            collectscript.FlashLightComplete = false;
        }

        if (collectscript.PoisonComplete && !PoisonAddedToInventory)
        {
            AddPosionTOInventory();
            collectscript.PoisonComplete = false;
        }
    }

    void AddSwordToInventory()
    {
       
        for (int i = 0; i < bInv.Length; i++)
        {
            if (!bInv[i])
            {
                bInv[i] = true;
                gInv[i].GetComponent<Image>().sprite = SwordOfTheForest;
                gInv[i].GetComponent<Image>().enabled = true; 
                gInv[i].GetComponent<Image>().color = Color.white;
                swordAddedToInventory = true;
                break;
            }
        }
    }

    void AddFlashlightToInventory()
    {
        for (int i = 0; i < bInv.Length; i++)
        {
            if (!bInv[i])
            {
                bInv[i] = true;
                gInv[i].GetComponent<Image>().sprite = Flashlihgt;
                gInv[i].GetComponent<Image>().enabled = true; 
                gInv[i].GetComponent<Image>().color = Color.white;
                FlashlightAddedToInventory = true; 
                break;
            }
        }
    }

    void AddPosionTOInventory()
    {
        for (int i = 0; i < bInv.Length; i++)
        {
            if (!bInv[i])
            {
                bInv[i] = true;
                gInv[i].GetComponent<Image>().sprite = Posion;
                gInv[i].GetComponent<Image>().enabled = true; 
                gInv[i].GetComponent<Image>().color = Color.white;
                PoisonAddedToInventory = true; 
                break;
            }
        }
    }
}
