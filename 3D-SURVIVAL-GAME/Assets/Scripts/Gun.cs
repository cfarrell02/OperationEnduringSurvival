using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Animator animator;
    public ParticleSystem ps;
    public AudioClip gunShot;
    private AudioSource audioSource;
   
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent < Animator > ();
    }

    // Update is called once per frame
    void Update()
    {
        ps.Stop(true);
        if (Input.GetButtonDown("Fire1"))
        {
            audioSource.PlayOneShot(gunShot);
            ps.Play();
            animator.SetBool("isFiring", true);
            animator.Play("Recoil");
        }
        else
        {
            animator.SetBool("isFiring", false);
            
        }
    }
}
