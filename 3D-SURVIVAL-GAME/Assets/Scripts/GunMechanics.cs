using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GunMechanics : MonoBehaviour
{
    
    public int maxAmmo = 200,ammo=50,magazineCapacity = 20;
    private int magazine, killCount = 0;
    public float damage = 10f, range = 100f,impactForce = 30f,fireRate=15f;
    public Camera fpsCam;
    public GameObject impactEffect;
    Gun gun;
    public TextMeshProUGUI text,scoreText;
    public PlayerHealth playerHealth;
    public GameObject magazineBar;
    private float nextTimeToFire = 0f,magazineBarMax;
    // Start is called before the first frame update
    void Start()
    {
        magazineBarMax = magazineBar.GetComponent<RectTransform>().sizeDelta.y;
        magazine = magazineCapacity;
       // ammo = maxAmmo;
        gun = GetComponent<Gun>();
        if (PlayerPrefs.HasKey("Kills")&&SceneManager.GetActiveScene().buildIndex!=1)
        {
            ammo = PlayerPrefs.GetInt("Ammo");
            magazine = PlayerPrefs.GetInt("Magazine");
            killCount = PlayerPrefs.GetInt("Kills");

        }
    }

    // Update is called once per frame
    void Update()
    {

        PlayerPrefs.SetInt("Ammo", ammo);
        PlayerPrefs.SetInt("Magazine", magazine);
        PlayerPrefs.SetInt("Kills", killCount);
        updateAmmoUI(magazine, ammo);
        scoreText.SetText("Kills: " + killCount);
        if (gun.isReloading()) return;
        if (ammo > maxAmmo) ammo = maxAmmo;
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

    void updateAmmoUI(int magazine, int ammo)
    {
        text.SetText(ammo.ToString());
        RectTransform rect = magazineBar.gameObject.GetComponent<RectTransform>();
        float height = magazineBarMax * magazine / magazineCapacity;
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

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
            if (hit.transform.parent.name.Equals(this.transform.name)) return;
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                bool dead = target.TakeDamage(damage);
                if (dead)
                {
                    killCount++;
                    playerHealth.touchingEnemy = false;
                }
            }
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal*impactForce);
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO.transform.SetParent(hit.transform);
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
