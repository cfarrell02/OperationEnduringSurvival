using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMechanics : MonoBehaviour
{
    public int ammo = 200,magazineCapacity = 20;
    private int magazine;
    public float damage = 10f, range = 100f;
    public Camera fpsCam;
    Gun gun;
    // Start is called before the first frame update
    void Start()
    {
        magazine = magazineCapacity;
        gun = GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gun.isReloading()) return;
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }

    }
    void Shoot()
    {
        print("Magazine: " + magazine + " / Reserve Ammo: " + ammo);
        if (magazine < 1) {
            Reload();
            return;
        }
        magazine -= 1;
        gun.Shoot();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
           // print(hit.transform.name);
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
