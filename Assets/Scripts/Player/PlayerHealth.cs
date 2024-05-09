using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    private Color orange;
    public float maxHealth = 100;
    public float chipSpeed = 2f;
    public Image frontHealth;
    public Image backhealth;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        orange = backhealth.color;
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if(Input.GetKeyDown(KeyCode.V)){
            takeDamage(Random.Range(5,10));
        }
        if(Input.GetKeyDown(KeyCode.B)){
            healDamage(Random.Range(5,10));
        }
    }

    //Checks if damage has been taken and updates UI
    public void UpdateHealthUI()
    {
        float frontFill = frontHealth.fillAmount;
        float backFill = backhealth.fillAmount;
        float hpFrac = health/maxHealth;
        //When damage is taken reduce front health immediately and also doing so but delaying back health 
        if(backFill > hpFrac){
            backhealth.color = orange;
            frontHealth.fillAmount = hpFrac;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            percentComplete *= percentComplete;
            backhealth.fillAmount = Mathf.Lerp(backFill, hpFrac, percentComplete);
        }
        else if(frontFill < hpFrac){
            backhealth.color = Color.green;
            backhealth.fillAmount = hpFrac;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            percentComplete *= percentComplete;
            frontHealth.fillAmount = Mathf.Lerp(frontFill, hpFrac, percentComplete);
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0;
    }

    public void healDamage(float heal)
    {
        health += heal;
        lerpTimer = 0;
    }
}
