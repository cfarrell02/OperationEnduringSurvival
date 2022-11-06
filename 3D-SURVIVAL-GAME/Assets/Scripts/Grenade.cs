using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Grenade : MonoBehaviour
{
    public GameObject grenade;
    public float throwForce = 15f;
    public int maxGrenades = 4,grenades;
    [SerializeField] private TextMeshProUGUI grenadeCount;
    private AudioSource audioSource;
    [SerializeField] private AudioClip grenadePickUp;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        grenades = 0;
    }

    // Update is called once per frame
    void Update()
    {
        grenadeCount.SetText(grenades.ToString());
        if (grenades > 0 && Input.GetButtonDown("Fire3"))
        {
            print("Grenade Thrown");
            Rigidbody clone = Instantiate(grenade, new Vector3(transform.position.x,transform.position.y-.1f,transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
            clone.GetComponent<GrenadeItem>().delay = 3;
            clone.velocity   = transform.forward * throwForce;
            grenades--;
        }   
    }

    public bool AddGrenades(int amountOfGrenades)
    {
        if (grenades == maxGrenades) return false;
        if (grenades + amountOfGrenades <= maxGrenades) grenades += amountOfGrenades;
        else grenades = maxGrenades;
        audioSource.PlayOneShot(grenadePickUp);
        return true;
    }

}
