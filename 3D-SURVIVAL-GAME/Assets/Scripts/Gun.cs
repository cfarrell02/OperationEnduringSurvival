using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    Animator animator;
    public ParticleSystem ps;
    public AudioClip gunShot, reload;
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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Reload"))
            return;
        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.Play("Reload");
            audioSource.PlayOneShot(reload);
        }
 
        else
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetBool("isAiming", true);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            animator.SetBool("isAiming", false);
        }
        if (Input.GetButtonDown("Fire1"))
        {
            audioSource.PlayOneShot(gunShot);
            ps.Play();
            animator.SetBool("isFiring", true);
            if (!animator.GetBool("isAiming"))
                animator.Play("Recoil");
            else
                animator.Play("Recoil Aimed");
        }
        else
        {
            
            animator.SetBool("isFiring", false);
            ps.Clear();
            ps.Stop();

        }

    }
}
