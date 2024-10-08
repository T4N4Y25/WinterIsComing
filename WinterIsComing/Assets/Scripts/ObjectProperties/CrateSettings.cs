using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            // Add a MeshCollider if it doesn't already have one
            if (child.GetComponent<MeshCollider>() == null)
            {
                child.gameObject.AddComponent<BoxCollider>();
            }

            // Add a Rigidbody if it doesn't already have one
            if (child.GetComponent<Rigidbody>() == null)
            {
                child.gameObject.AddComponent<Rigidbody>();
            }
        }
    
    }
}

    // Update is called once per frame
 

