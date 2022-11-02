using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadePickup : MonoBehaviour
{
    [SerializeField] private int grenadeAmount = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Grenade grenade = other.GetComponentInChildren<Grenade>();
            if (grenade.AddGrenades(grenadeAmount)) Destroy(gameObject);
        }
    }
}
