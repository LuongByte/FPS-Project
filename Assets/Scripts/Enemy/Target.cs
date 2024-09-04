using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float health = 100f;
    
    public void takeDamage(float damage)
    {
        health -= damage;
        if(health <= 0f){
            Destroy(gameObject);
        }
        
        
    }
}