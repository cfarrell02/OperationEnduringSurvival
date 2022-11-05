using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f, detectionRadius=.4f;
    private float health;
    private StarterAssets.FirstPersonController controller;
    public UI uiController;
    public LayerMask enemyLayer;
    private bool touchingEnemy;
    private float prevVelocity, damageCoolDown = .5f, lastDamage;
    private int diffficultyLevel;
    public GameObject healthBar;
    // Start is called before the first frame update
    void Start()
    {
        //detectionRadius = GetComponent<CapsuleCollider>().radius;
        diffficultyLevel = PlayerPrefs.GetInt("Difficulty");
        controller = GetComponent<StarterAssets.FirstPersonController>();
        health = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        if (PlayerPrefs.HasKey("Health") && PlayerPrefs.GetFloat("Health") > 0 && SceneManager.GetActiveScene().buildIndex != 1) 
        {
            maxHealth = PlayerPrefs.GetFloat("MaxHealth");
            health = PlayerPrefs.GetFloat("Health");
        }
       
        PlayerPrefs.SetFloat("MaxHealth", maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
        PlayerPrefs.SetFloat("Health", health);
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
            Die();
            return;
        }
        touchingEnemy = Physics.CheckSphere(transform.position, detectionRadius, enemyLayer.value);
        if (touchingEnemy && Time.time-lastDamage > damageCoolDown)
        {
            TakeDamage(10 + 10 * diffficultyLevel);
            lastDamage = Time.time;
        }
            health -= ProcessFallDamage();


    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    void Die()
    {

        uiController.returnToMenu();
    }

    int ProcessFallDamage()
    {
        float velocityChange = controller._verticalVelocity - prevVelocity;
        prevVelocity = controller._verticalVelocity;
        return velocityChange < 15 ? 0 : (int)(velocityChange * 1.5);
    }


    public bool AddHealth(float healthToAdd)
    {
        if (health >= maxHealth) return false;
        float missingHealth = maxHealth - health;
        if (healthToAdd < missingHealth) health += healthToAdd;
        else health += missingHealth;
        return true;
    }



}
