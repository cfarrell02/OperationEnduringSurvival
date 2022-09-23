using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
     Animator animator;
   
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent < Animator > ();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetBool("isFiring", true);
        }
        else animator.SetBool("isFiring", false);
    }
}
