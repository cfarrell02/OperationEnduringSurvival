using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammunition : MonoBehaviour
{
    public int ammunitionVal = 100;
    public AudioClip ammoNoise;
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
        
        Transform gun = other.transform.Find("PlayerCameraRoot").Find("Main Camera").GetChild(0);
        GunMechanics gunMechanics = gun.GetComponent<GunMechanics>();
        if (gunMechanics.ammo >= gunMechanics.maxAmmo) return;
        gunMechanics.ammo += ammunitionVal;
        gun.GetComponent<AudioSource>().PlayOneShot(ammoNoise,1f);
        Destroy(this.gameObject);
        
    }
}
