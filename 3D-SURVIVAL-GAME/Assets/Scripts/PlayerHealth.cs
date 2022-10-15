using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private float health;
    private StarterAssets.FirstPersonController controller;
    private float prevVelocity;
    public GameObject healthBar;
    public TextMeshProUGUI text;
    private bool enemyAttacking;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<StarterAssets.FirstPersonController>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        float width = (health / maxHealth) * Screen.width;
        RectTransform rectTransform = healthBar.gameObject.GetComponent<RectTransform>();
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        Image image = healthBar.gameObject.GetComponent<Image>();
        float greenVal =  (health / maxHealth) *.4f;
        float redVal = 1-(health / maxHealth) ;
        image.color = new Vector4(redVal, greenVal, 0f,1f);
        if (health <= 0)
        {
            health = 0;
            die();
            return;
        }
        health -= processFallDamage();
        if (enemyAttacking) health-=.2f;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            enemyAttacking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Enemy")
        {
            enemyAttacking = false;
        }
    }

    public bool addHealth(float healthToAdd)
    {
        if (health >= maxHealth) return false;
        float missingHealth = maxHealth - health;
        if (healthToAdd < missingHealth) health += healthToAdd;
        else health += missingHealth;
        return true;
    }


}
