using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Button : Interactable
{
    private bool unlocked;
    private GameObject player;
    private PlayerInventory inventory;
    [SerializeField] private GameObject door;
    //Green = 0, Red = 1, Blue = 2, Yellow(Can't be opened by AI) = 3
    [SerializeField] private float color;
    [SerializeField] private NavMeshObstacle obstacle;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        inventory = player.GetComponent<PlayerInventory>();
        if(color == 0)
            unlocked = true;
        if(color == 3){
            obstacle.carveOnlyStationary = true;
            obstacle.carving = true;
            obstacle.enabled = true;
        }
    }


    private void Open(){
        bool state = door.GetComponent<Animator>().GetBool("IsOpen");
        door.GetComponent<Animator>().SetBool("IsOpen", !state);
        if(color == 3){
            obstacle.carveOnlyStationary = state;
            obstacle.carving = state;
            obstacle.enabled = state;
        }
    } 
    protected override void Interact()
    {
        if(unlocked){
            Open();
        }
        else{
            if(inventory.CheckCard(color) == true && !unlocked){
                unlocked = true;
                Open();
            }
        }
    }

}
