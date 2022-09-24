using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Animator animator;
    public ParticleSystem particleSystem;
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
        if (Input.GetButtonDown("Fire1"))
        {
            audioSource.PlayOneShot(gunShot);
            particleSystem.Play();
            animator.SetBool("isFiring", true);
        }
        else
        {
            animator.SetBool("isFiring", false);
            particleSystem.Stop();
        }
    }
}
