using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject pnlInventory;
    public GameObject pnlQuest;
    // Start is called before the first frame update
    void Start()
    {
        pnlQuest.SetActive(false);

    }

    public void SwitchToQuest()
    {
        pnlInventory.SetActive(false);
        pnlQuest.SetActive(true);
    }

    public void SwitchToInventory()
    {
        pnlInventory.SetActive(true);
        pnlQuest.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
