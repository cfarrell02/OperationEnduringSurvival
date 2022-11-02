using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

    public class FollowScript: MonoBehaviour
    {
        [SerializeField] GameObject target;
        [SerializeField] private float speed = 1.5f;
        [SerializeField] private float detectionDistance = 10f;
        [SerializeField] private GameObject[] waypoints;
        private int wayPointIndex,difficultyLevel;
        private bool atWaypoint = true,waiting;
        private Animator animator;
        private NavMeshAgent navMeshAgent;
        private Ray ray;
        private RaycastHit hit;
        void Start()
        {
        speed += (difficultyLevel - 1);
        difficultyLevel = PlayerPrefs.GetInt("Difficulty");
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        wayPointIndex = Random.Range(0, waypoints.Length);
        // this.transform.position = waypoints[wayPointIndex].transform.position;
        StartCoroutine(WaitAtWaypoint(1));

        }
    void Update()
    {

        ray.origin = gameObject.transform.position + Vector3.up;
        ray.direction = gameObject.transform.forward;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);


        if (Physics.Raycast(ray.origin, ray.direction * 100, out hit))
        {
            float distance = Vector3.Distance(this.transform.position, target.transform.position);

            if (hit.collider.gameObject.name == target.name || distance < detectionDistance + 10*difficultyLevel)
            {
                navMeshAgent.destination = target.transform.position;
                animator.SetBool("Walking", true);
            }


            else
            {
                float distanceToWaypoint = Vector3.Distance(this.transform.position, waypoints[wayPointIndex].transform.position);

                if (!atWaypoint && distanceToWaypoint < 4)
                {
                    atWaypoint = true;
                    animator.SetBool("Walking", false);

                }
                if (atWaypoint && !waiting)
                {

                    StartCoroutine(WaitAtWaypoint(Random.Range(10, 120)));
                    waiting = true;
                }

            }
        }
    }

        private IEnumerator WaitAtWaypoint(float time)
        {
        yield return new WaitForSeconds(time);
        waiting = false;
        navMeshAgent.destination = waypoints[wayPointIndex].transform.position;
        wayPointIndex = (wayPointIndex+1)%waypoints.Length;
        animator.SetBool("Walking", true);
    }
    }

