using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeLoud: Interactable
{
    [SerializeField]
    private GameObject heli;
    [SerializeField]
    private ProgressController controller;
    protected override void Interact()
    {
        heli.SetActive(true);
        controller.UpdateProgress(1);
        gameObject.SetActive(false);
    }

}
