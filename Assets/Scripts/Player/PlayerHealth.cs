using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float lerpTimer;
    private float dmgTimer;
    private Color orange;
    [Header("Healthbar")]
    public float maxHealth = 100;
    public float chipSpeed = 2f;
    public Image frontHealth;
    public Image backHealth;
    public TextMeshProUGUI healthNum;
    [Header("Damage Overlay")]
    public Image dmgOverlay;
    public float duration;
    public float fadeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        orange = backHealth.color;
        dmgOverlay.color = new Color(dmgOverlay.color.r, dmgOverlay.color.g, dmgOverlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if(dmgOverlay.color.a > 0){
            if(health >= 20){
                dmgTimer += Time.deltaTime;
                if(dmgTimer > duration){
                    float tempAlpha = dmgOverlay.color.a;
                    tempAlpha -= Time.deltaTime * fadeSpeed;
                    dmgOverlay.color = new Color(dmgOverlay.color.r, dmgOverlay.color.g, dmgOverlay.color.b, tempAlpha);
                }
                return;
            }
            else{
                return;
            }
        }
    }

    //Checks if damage has been taken and updates UI
    public void UpdateHealthUI()
    {
        float frontFill = frontHealth.fillAmount;
        float backFill = backHealth.fillAmount;
        float hpFrac = health/maxHealth;
        //When damage is taken reduce front health immediately and also doing so but delaying back health 
        if(backFill > hpFrac){
            backHealth.color = orange;
            frontHealth.fillAmount = hpFrac;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            percentComplete *= percentComplete;
            backHealth.fillAmount = Mathf.Lerp(backFill, hpFrac, percentComplete);
        }
        else if(frontFill < hpFrac){
            backHealth.color = Color.green;
            backHealth.fillAmount = hpFrac;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer/chipSpeed;
            percentComplete *= percentComplete;
            frontHealth.fillAmount = Mathf.Lerp(frontFill, hpFrac, percentComplete);
        }
        healthNum.text = Mathf.Round(health).ToString();
    }
    public void takeDamage(float damage)
    {
        health -= damage;
        if(health < 20){
            dmgOverlay.color = new Color(dmgOverlay.color.r, dmgOverlay.color.g, dmgOverlay.color.b, 1);
        }
        else{
            dmgOverlay.color = new Color(dmgOverlay.color.r, dmgOverlay.color.g, dmgOverlay.color.b, 0.7f);
        }
        lerpTimer = 0;
        dmgTimer = 0;
        
    }

    public void healDamage(float heal)
    {
        health += heal;
        lerpTimer = 0;
    }
}
