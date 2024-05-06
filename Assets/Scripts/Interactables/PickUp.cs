using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : Interactable
{
    public GameObject pickUp;
    
    protected override void Interact()
    {
        Destroy(pickUp);
    }
}
