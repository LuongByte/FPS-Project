using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeStealth: Interactable
{
    private bool doorOpen;
    [SerializeField]
    private GameObject gate;
    [SerializeField]
    private ProgressController controller;
    void Start()
    {
        gate.GetComponent<Animator>().SetBool("IsOpen", true);
    }

    public void CloseGate()
    {
        gate.GetComponent<Animator>().SetBool("IsOpen", false);
    }
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        gate.GetComponent<Animator>().SetBool("IsOpen", true);
        DisableInteract();
        controller.EnableEscape();
    }

}
