using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject grenade;
    public float throwForce = 10f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            print("Grenade Thrown");
            Rigidbody clone = Instantiate(grenade, new Vector3(transform.position.x,transform.position.y-.1f,transform.position.z), Quaternion.identity).GetComponent<Rigidbody>();
            clone.velocity   = transform.forward * throwForce;
        }   
    }

    IEnumerator destroyAfter(GameObject item, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(item);
    }
}
