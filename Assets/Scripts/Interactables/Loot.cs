using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : Interactable
{
    private GameObject player;
    private Inventory inventory;
    [SerializeField]
    private float value;   
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
    }
    protected override void Interact()
    {
        inventory.CollectLoot(value);
        Destroy(gameObject);
    }
}