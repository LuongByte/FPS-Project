using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable
{
    private bool doorOpen, unlocked;
    private bool special;
    private GameObject player;
    [SerializeField]
    private GameObject door;
    private PlayerInventory inventory;
    //Red = 1, Green = 2, Blue = 3
    [SerializeField]
    private float color;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<PlayerInventory>();
        special = false;
        if(color == 0)
            unlocked = true;
    }

    public void SpecialUnlock(string newPrompt)
    {
        promptMessage = newPrompt;
        unlocked = true;
        special = true;
    }

    protected override void Interact()
    {
        if(unlocked){
            doorOpen = !doorOpen;
            door.GetComponent<Animator>().SetBool("IsOpen", doorOpen);
                if(special)
                    DisableInteract();
        }

        if(inventory.CheckCard(color) == true && !unlocked){
            unlocked = true;
        }
    }

}
