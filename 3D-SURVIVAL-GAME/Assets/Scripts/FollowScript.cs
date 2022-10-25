using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace UnityStandardAssets.Utility
{
    public class FollowScript: MonoBehaviour
    {
        [SerializeField] GameObject target;
        [SerializeField] private float speed = 1.5f;
        [SerializeField] private float detectionDistance = 10f;
        [SerializeField] private GameObject[] waypoints = new GameObject[4];
        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private Ray ray;
        private RaycastHit hit;
        void Start()
        {
            animator = GetComponentInChildren<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            for(int i = 0; i < 4; ++i)
            {
                waypoints[0] = new GameObject(name+" - Waypoint " + i);
                waypoints[0].transform.position = transform.position + new Vector3(50*i+10,0,0);
            }
        }
        void Update()
        {

            ray.origin = gameObject.transform.position + Vector3.up;
            ray.direction = gameObject.transform.forward;
            Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);


            if (Physics.Raycast(ray.origin, ray.direction * 100, out hit))
            {
                float distance = Vector3.Distance(this.transform.position, target.transform.position);

                if (hit.collider.gameObject.name == target.name || distance < detectionDistance)
                {
                    navMeshAgent.destination = target.transform.position;
                    animator.SetBool("Walking", true);
                }

            }
            else
            {
                int index = Random.Range(0, waypoints.Length - 1);
                navMeshAgent.destination = waypoints[index].transform.position;
            }
        }
    }
}
