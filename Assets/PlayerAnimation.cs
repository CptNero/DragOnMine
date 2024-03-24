using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    Animator animator;
    bool digging = false;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space) && animator.GetBool("IsDigging") == false)
        {
            animator.SetBool("IsDigging", true);
        }
        else 
        {
            animator.SetBool("IsDigging", false);
        }
        
        
    }
}
