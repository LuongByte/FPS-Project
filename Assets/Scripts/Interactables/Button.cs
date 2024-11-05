using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable
{
    private bool doorOpen;
    private GameObject player;
    [SerializeField]
    private GameObject door;
    private Inventory inventory;
    //Red = 1, Green = 2, Blue = 3
    [SerializeField]
    private float color;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<Inventory>();
    }
    protected override void Interact()
    {
        if(inventory.CheckCard(color) == true){
            doorOpen = !doorOpen;
            door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
        }
    }

}
