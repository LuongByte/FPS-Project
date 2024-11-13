using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private GameObject parent;
    private bool spawnedIn;
    private DropItems itemdrop;
    private Enemy enemy;
    private EnemyController controller;
    [SerializeField]
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        itemdrop = GetComponent<DropItems>();
        enemy = GetComponent<Enemy>();
        controller = enemy.controller;
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        enemy.CheckDamage();
        if(health <= 0){
            Debug.Log("Drop");
            itemdrop.Drop();
            controller.UpdateKill();
            Destroy(gameObject);
            if(!spawnedIn)
                Destroy(transform.parent.gameObject);
        }
    }

    public void Spawned()
    {
        spawnedIn = true;
    }
}
