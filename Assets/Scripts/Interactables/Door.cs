using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Open : Interactable
{
    [SerializeField]
    GameObject closedObject;

    protected override void Interact()
    {
        closedObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
