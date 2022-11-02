using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeItem : MonoBehaviour
{
    public float delay = 3f,blastRadius = 5f,force = 700f;
    public GameObject explosionEffect;
    private float countdown;
    private bool hasExploded = false;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }
    void Explode()
    {
        GameObject explosion = Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);
        foreach(Collider item in colliders)
        {
            Rigidbody rb = item.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, blastRadius);
            }
            if (item.gameObject.CompareTag("Enemy"))
            {
                Target enemy = item.GetComponent<Target>();
                float damage = 100/Vector3.Distance(transform.position, item.transform.position);

                enemy.TakeDamage(damage);
            }
        }
        Destroy(this.gameObject);
        StartCoroutine(DestroyAfter(explosion, 4));
    }
    IEnumerator DestroyAfter(GameObject item, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(item);
    }
}
