using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class for interactables
public abstract class Interactable : MonoBehaviour
{
    //Used to Add or remove InteractionEvent objects
    public bool useEvents;
    [SerializeField]
    public string promptMessage;
    //Will be called by player
    public void BaseInteract()
    {
        if(useEvents){
            GetComponent<InteractionEvent>().onInteract.Invoke();
        }
        Interact();
    }

    protected virtual void Interact()
    {

    }
}
