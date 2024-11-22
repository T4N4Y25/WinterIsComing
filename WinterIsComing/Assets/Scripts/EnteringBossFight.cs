using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnteringBossFight : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Nyxie;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Nyxie.text = "";
            Nyxie.enabled = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Nyxie.enabled = true;
        Nyxie.text = "This is it, the final showdown. I hope you're ready cause I am not!";
    }
}
