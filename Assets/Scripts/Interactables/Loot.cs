using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Loot : Interactable
{
    private GameObject player;
    private PlayerInventory inventory;
    [SerializeField]
    private float value;
    private string full;
    private string avail;
    void Start()
    {
        avail = promptMessage;
        full = "Inventory Full";
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if(inventory.Full(value)){
            promptMessage = full;
            DisableInteract();
        }
        else{
            promptMessage = avail;
            EnableInteract();
        }
    }
    protected override void Interact()
    {   
        inventory.CollectLoot(value);
        Destroy(gameObject);
    }
}
