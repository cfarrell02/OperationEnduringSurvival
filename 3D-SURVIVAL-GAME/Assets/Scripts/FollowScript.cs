using System;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Utility
{
    public class FollowScript: MonoBehaviour
    {
        [SerializeField] GameObject target;
        [SerializeField] private float speed = 1.5f;
        [SerializeField] private float detectionDistance = 50f;
        private Animator animator;
        private NavMeshAgent navMeshAgent;
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        void Update()
        {
            float distance = Vector3.Distance(this.transform.position, target.transform.position);
            if (distance < detectionDistance)
            {
               // transform.LookAt(target.transform);
                navMeshAgent.destination = target.transform.position;
                //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
                animator.SetBool("Walking", true);
            }
        }
    }
}
