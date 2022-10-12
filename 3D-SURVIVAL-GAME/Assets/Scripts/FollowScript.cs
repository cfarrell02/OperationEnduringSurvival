using System;
using UnityEngine;


namespace UnityStandardAssets.Utility
{
    public class FollowScript: MonoBehaviour
    {
        [SerializeField] GameObject target;
        [SerializeField] private float speed = 1.5f;


        void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
}
