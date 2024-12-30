using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBag : Interactable
{
    [SerializeField] private GameObject Fuse;
    [SerializeField] private ProgressController controller;
    void Start()
    {
        Fuse.SetActive(false);
    }
    protected override void Interact()
    {
        Fuse.SetActive(true);
        controller.UpdateProgress(3);
        DisableInteract();
    }
}
