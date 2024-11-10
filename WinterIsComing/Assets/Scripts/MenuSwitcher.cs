using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject pnlInventory;
    public GameObject pnlQuest;
    public GameObject pnlMap;
    // Start is called before the first frame update
    void Start()
    {
        pnlQuest.SetActive(false);
        pnlMap.SetActive(false);

    }

    public void SwitchMain()
    {
         SceneManager.LoadScene("MainMenu");
    }

    public void SwitchToQuest()
    {
        pnlInventory.SetActive(false);
        pnlMap.SetActive(false);
        pnlQuest.SetActive(true);
    }

    public void SwitchToInventory()
    {
        pnlInventory.SetActive(true);
        pnlQuest.SetActive(false);
        pnlMap.SetActive(false);
    }

    public void SwitchToMap()
    {
        pnlInventory.SetActive(false);
        pnlQuest.SetActive(false);
        pnlMap.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
