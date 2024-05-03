using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for interactables
public abstract class Interactable : MonoBehaviour
{
    public string promptMessage;
    //Will be called by player
    public void BaseInteract()
    {
        Interact();
    }

    protected virtual void Interact()
    {

    }
}
