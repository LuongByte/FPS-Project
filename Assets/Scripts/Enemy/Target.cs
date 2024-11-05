using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    private float health;
    private DropItems itemdrop;
    // Start is called before the first frame update
    void Start()
    {
        itemdrop = GetComponent<DropItems>();
    }

    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            Debug.Log("Drop");
            itemdrop.Drop();
            Destroy(gameObject);
        }
    }
}
