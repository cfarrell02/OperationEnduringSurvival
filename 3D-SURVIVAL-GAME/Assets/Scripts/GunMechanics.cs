using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GunMechanics : MonoBehaviour
{
    public int ammo = 200,magazineCapacity = 20;
    private int magazine;
    public float damage = 10f, range = 100f,impactForce = 30f,fireRate=15f;
    public Camera fpsCam;
    public GameObject impactEffect;
    Gun gun;
    public TextMeshProUGUI text;
    private float nextTimeToFire = 0f;
    // Start is called before the first frame update
    void Start()
    {
        magazine = magazineCapacity;
        gun = GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText("Magazine: " + magazine + "\n Reserve Ammo: " + ammo);
        if (gun.isReloading()) return;
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

    }
    void Shoot()
    {
        if (magazine < 1) {
            Reload();
            return;
        }
        magazine -= 1;
        gun.Shoot();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
          //  if (hit.transform.parent!=null || hit.transform.parent.name.Equals(this.transform.name)) return;
           Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal*impactForce);
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 5f);
        }

    }
    void Reload()
    {
        if (ammo <= 0 || magazine == magazineCapacity) return;
        ammo -= (magazineCapacity-magazine);
        if (ammo < 0) ammo = 0;
        magazine = 20;
        gun.Reload();
    }
}
