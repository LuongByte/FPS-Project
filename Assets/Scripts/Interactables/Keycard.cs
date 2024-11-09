using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : Interactable
{
    private GameObject player;
    private PlayerInventory inventory;
    //Red = 1, Green = 2, Blue = 3
    [SerializeField]
    private float color;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<PlayerInventory>();
    }
    protected override void Interact()
    {
        inventory.CollectCard(color);
        Destroy(gameObject);
    }
}
