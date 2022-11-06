using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{

    public float damage = 100f;
    private Animator animator;
    [SerializeField] private AnimationClip[] attackAnimations;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    void Attack()
    {
        int index = Random.Range(0, attackAnimations.Length );
        animator.Play(attackAnimations[index].name);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Knife Idle") && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Target>().TakeDamage(damage);
        }
    }
}
