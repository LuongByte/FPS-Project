using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collect : Interactable
{
    public GameObject collect;
    
    protected override void Interact()
    {
        Destroy(collect);
    }
}
