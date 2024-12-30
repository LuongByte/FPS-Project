using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RemoteLock : Interactable
{
    private bool unlocked;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private ProgressController progressController;
    void Start()
    {
        unlocked = false;
    }

    public void RemoteUnlock(string newPrompt)
    {
        promptMessage = newPrompt;
        unlocked = true;
    }

    private void Open(){
        bool state = door.GetComponent<Animator>().GetBool("IsOpen");
        door.GetComponent<Animator>().SetBool("IsOpen", !state);
        DisableInteract();
        progressController.UpdateProgress(1);
    } 
    protected override void Interact()
    {
        if(unlocked){
            Open();
        }
    }

}
