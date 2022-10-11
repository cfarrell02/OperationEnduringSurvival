using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int health;
    private StarterAssets.FirstPersonController controller;
    private float prevVelocity;
    public TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StarterAssets.FirstPersonController>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        text.SetText("" + health);
        if (health <= 0)
        {
            health = 0;
            die();
            return;
        }
        health -= processFallDamage();
    }
    void die()
    {
        print("Dead");
    }

    int processFallDamage()
    {
        float velocityChange = controller._verticalVelocity - prevVelocity;
        prevVelocity = controller._verticalVelocity;
        return velocityChange < 15 ? 0 : (int)(velocityChange * 1.5);
    }


}
