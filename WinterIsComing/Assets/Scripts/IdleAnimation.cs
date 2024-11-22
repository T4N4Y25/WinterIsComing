using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAnimation : MonoBehaviour
{
    private Animator anim;
    [SerializeField] Animator anim2;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("IsChasing",false);
        anim.SetBool("IsWalking", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            anim2.SetTrigger("Activate");
        }
    }
}
