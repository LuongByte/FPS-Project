using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : Interactable
{
    private bool doorOpen, unlocked;
    private GameObject player;
    [SerializeField]
    private GameObject gate;
    [SerializeField]
    private ProgressController controller;


    protected override void Interact()
    {
        doorOpen = !doorOpen;
        gate.GetComponent<Animator>().SetBool("IsOpen", true);
        DisableInteract();
        controller.EnableEscape();
    }

}
