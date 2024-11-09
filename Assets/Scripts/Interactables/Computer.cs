using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Computer : Interactable
{
    [SerializeField]
    Button keyPad;
    [SerializeField]
    private string newPrompt;
    protected override void Interact()
    {
        keyPad.SpecialUnlock(newPrompt);
    }
}
