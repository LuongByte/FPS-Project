using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : Interactable
{
    private float despawnTimer;
    private PlayerShoot shoot;
    private bool dropped;
    [SerializeField]
    private GameObject pickUp;
    void Start(){
        dropped = false;
        despawnTimer = 0;
    }
    void Update(){
        if(dropped && LayerMask.NameToLayer("Interactable") == gameObject.layer){
            if(despawnTimer >= 30f){
                Destroy(gameObject);
            }
            else
                despawnTimer += Time.deltaTime;
        }
    }
    protected override void Interact()
    {
        shoot = FindObjectOfType<PlayerShoot>();
        despawnTimer = 0;
        shoot.PickUp(pickUp);
        foreach (Transform child in transform){
            child.gameObject.layer = LayerMask.NameToLayer("Holding");
            foreach (Transform child2 in child.transform)
                child2.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    public void Dropped()
    {
        dropped = true;
        EnableInteract();
        foreach (Transform child in transform){
            child.gameObject.layer = LayerMask.NameToLayer("Default");
            foreach (Transform child2 in child.transform)
                child2.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
