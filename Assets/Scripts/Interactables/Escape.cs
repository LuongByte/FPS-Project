using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Escape : Interactable
{
    private bool doorOpen;
    [SerializeField]
    private GameObject gate;
    [SerializeField]
    private GameObject spawnpoint;
    [SerializeField]
    private ProgressController controller;


    protected override void Interact()
    {
        spawnpoint.SetActive(false);
        doorOpen = !doorOpen;
        gate.GetComponent<Animator>().SetBool("IsOpen", true);
        DisableInteract();
        controller.EnableEscape();
    }

}
