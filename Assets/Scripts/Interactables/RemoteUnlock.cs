using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RemoteUnlock : Interactable
{
    [SerializeField]RemoteLock keyPad;
    [SerializeField]private string newPrompt;
    [SerializeField]ProgressController controller;
    protected override void Interact()
    {
        keyPad.RemoteUnlock(newPrompt);
        DisableInteract();
        controller.UpdateProgress(3);
    }
}
